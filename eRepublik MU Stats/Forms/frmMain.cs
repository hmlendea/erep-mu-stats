using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Threading;
using eRepublik.BBCodes;
using eRepublik.Citizen;
using eRepublik.Citizen.Achievements;

namespace eRepublik_MU_Stats
{
    public partial class frmMain : Form
    {
        NumberFormatInfo numberFormat = new NumberFormatInfo();
        DateTime scanDateBegin, scanDateFinish;
        String LogDateFormat = "<hh:mm:ss> ";

        public Citizen[] member;

        public string[] RankConstants;
        private int members;
        private bool scanning;

        public frmMain()
        {
            InitializeComponent();
            GetRankConstants();
            numberFormat.NumberGroupSeparator = ",";
            numberFormat.NumberDecimalSeparator = ".";
        }
        public void DeleteUnusedCitizens()
        {
            if (File.Exists("Members.TXT"))
            {
                string[] id = File.ReadAllLines("Members.TXT");

                foreach (string file in Directory.GetFiles("Data\\Citizens\\", "*.XML"))
                    if (Array.IndexOf(id, Path.GetFileNameWithoutExtension(file)) < 0)
                        File.Delete(file);
            }
        }
        public void BackUpCitizens()
        {
            if (File.Exists("Members.TXT"))
            {
                string[] id = File.ReadAllLines("Members.TXT");

                if (Directory.Exists("Data//Citizens//BackUp"))
                    Directory.Delete("Data//Citizens//BackUp", true);

                Directory.CreateDirectory("Data//Citizens//BackUp");

                foreach (string ctz in id)
                {
                    if(File.Exists("Data//Citizens//" + ctz + ".XML"))
                        File.Copy(
                            "Data//Citizens//" + ctz + ".XML",
                            "Data//Citizens//BackUp//" + ctz + ".XML");
                }
            }
        }

        #region Rank
        private void GetRankConstants()
        {
            XmlDocument xmlRanks = new XmlDocument();
            xmlRanks.Load("Data\\Rank Constants.XML");

            XmlNodeList xmlRanksNodes = xmlRanks.SelectNodes("RankConstants/Rank");
            RankConstants = new string[xmlRanksNodes.Count];
            int rnk = 0;

            foreach (XmlElement e in xmlRanksNodes)
            {
                RankConstants[rnk] = e.SelectSingleNode("Name").InnerText;
                rnk++;
            }
        }
        private int GetRankValue(string rank)
        {
            for (int i = 0; i < RankConstants.Length; i++)
                if (RankConstants[i] == rank)
                    return i + 1;

            return 0;
        }
        #endregion

        #region Scanning
        private void GetMembers()
        {
            string[] id = new string[1];
            if (File.Exists("Members.TXT"))
                id = File.ReadAllLines("Members.TXT");
            else
                File.Create("Members.TXT");

            members = id.Length;

            member = new Citizen[members];

            for (int ctz = 0; ctz < members; ctz++)
            {
                if (id[ctz] != "")
                {
                    member[ctz] = new Citizen();

                    member[ctz].ID = Convert.ToInt32(id[ctz]);

                    ListViewItem lvi = new ListViewItem(member[ctz].ID.ToString());
                    for (int i = 0; i <= lvMembers.Columns.Count; i++)
                        lvi.SubItems.Add("");
                    lvMembers.Items.Add(lvi);

                    member[ctz].Load("Data//Citizens//" + member[ctz].ID + ".XML");
                    member[ctz].Scan();
                    UpdateCitizenInList(ctz);
                    member[ctz].Save("Data//Citizens//" + member[ctz].ID + ".XML");

                    Log.AppendText(DateTime.Now.ToString(LogDateFormat) + "Scanning... " + ((ctz * 100) / members) + "%" + Environment.NewLine);
                }
            }

            DeleteUnusedCitizens();
            BackUpCitizens();
        }
        private void UpdateCitizenInList(int ctz)
        {
            for (int i = 1; i < lvMembers.Columns.Count; i++)
            {
                string val = "";

                switch (lvMembers.Columns[i].Text)
                {
                    case "Name":
                        val = member[ctz].Name;
                        break;

                    case "Profile Link":
                        val = member[ctz].ProfileLink;
                        break;

                    case "Strength":
                        val = member[ctz].Strength.ToString();
                        break;

                    case "Strength +":
                        val = member[ctz].StrengthGained.ToString();
                        break;

                    case "Rank":
                        val = member[ctz].Rank;
                        break;

                    case "Rank Pts":
                        val = member[ctz].RankPoints.ToString();
                        break;

                    case "Rank Pts +":
                        val = member[ctz].RankPointsGained.ToString();
                        break;

                    case "Influence":
                        val = member[ctz].Influence.ToString();
                        break;

                    case "Hit":
                        val = member[ctz].Hit.ToString();
                        break;

                    case "Xp":
                        val = member[ctz].Experience.ToString();
                        break;

                    case "Xp +":
                        val = member[ctz].ExperienceGained.ToString();
                        break;

                    case "LvL":
                        val = member[ctz].Level.ToString();
                        break;

                    case "Nat Rank":
                        val = member[ctz].NationalRank.ToString();
                        break;
                }

                if (val != "")
                    lvMembers.Items[ctz].SubItems[i].Text = val;

                lvMembers.Columns[i].Width = -2;
            }
        }
        #endregion

        #region Buttons
        private void btnScan_Click(object sender, EventArgs e)
        {
            if (!scanning)
            {
                scanning = true;
                scanDateBegin = DateTime.Now;

                lvMembers.Items.Clear();
                Log.Clear();
                GetMembers();

                scanDateFinish = DateTime.Now;
                scanning = false;

                Log.AppendText(DateTime.Now.ToString(LogDateFormat) + "Scan finished!" + Environment.NewLine);
                MessageBox.Show(
                    "Scan finished!",
                    "Scan finished!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show(
                        "You have to wait for the current scan to finish before you can start a new one!",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnWriteArticle_Click(object sender, EventArgs e)
        {
            if (!scanning && members > 0)
            {
                if (File.Exists("Data\\Design.TXT"))
                {
                    GenerateArticle();

                    Log.AppendText(DateTime.Now.ToString(LogDateFormat) + "Article finished!" + Environment.NewLine);

                    MessageBox.Show(
                        "Article finished and saved to 'Article.TXT', ready to be published!",
                        "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show(
                        "The article cannot be generated because 'Data\\Design.TXT' cannot be accessed",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show(
                    "The scan is not yet finished!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnHelp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "1.) Write the IDs of the citizens you wish to scan in 'Members.TXT', one on a line" + Environment.NewLine +
                "2.) Provide a 'Design.TXT' in the 'Data' folder" + Environment.NewLine +
                "3.) Click 'Scan' and wait for it to finish" + Environment.NewLine +
                "4.) Click 'Write Article' and get your article code from 'Article.TXT" + Environment.NewLine + Environment.NewLine +
                "Do you want to open the ReadMe for more detailed information?",
                "How to use", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                System.Diagnostics.Process.Start(@"ReadMe.TXT");
        }
        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "This totally awesome application was made in Visual Studio 2012 Professional, C# Language, on the .NET Framework 2.0 Platform, by a cool romanian guy named Mlendea Horatiu" + Environment.NewLine + Environment.NewLine +
                "Special thanks to my Military Unit, Dracones, for being so awesome and making me want to create this app for them to have stats once again \\o/",
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Article generation
        private string separator = " — ";

        private void GenerateArticle()
        {
            string article = File.ReadAllText("Data\\Design.TXT");

            #region General statistics variables
            long infTotal = 0, infAverage = 0;
            int actTotal = 0, actPercent = 0;
            int strTotal = 0, strAverage = 0;
            int hitTotal = 0, hitAverage = 0;
            int lvlTotal = 0, lvlAverage = 0;
            int rnkTotal = 0, rnkAverage = 0;
            int mdlTotal = 0, mdlNew = 0;
            int bmbTotal = 0;
            #endregion
            #region Retrieve general statistics
            for (int ctz = 0; ctz < members; ctz++)
            {
                infTotal += member[ctz].Influence;
                strTotal += member[ctz].Strength;
                hitTotal += member[ctz].Hit;
                lvlTotal += member[ctz].Level;
                rnkTotal += member[ctz].RankValue;
                bmbTotal += member[ctz].BombsUsed.Total;

                if (member[ctz].Influence > 0)
                    actTotal += 1;

                mdlTotal += member[ctz].Medals.Total;
                mdlNew += member[ctz].MedalsGained.Total;
            }

            infAverage = infTotal / members;
            actPercent = (actTotal * 100) / members;
            strAverage = strTotal / members;
            hitAverage = hitTotal / members;
            lvlAverage = lvlTotal / members;
            rnkAverage = rnkTotal / members;
            #endregion
            #region Replace tags with data
            Array.Sort<Citizen>(member, (x, y) => y.TopCampaignDamage.Damage.CompareTo(x.TopCampaignDamage.Damage));
            article = article
                .Replace("[TOP_CAMPAIGN_DAMAGE_PLAYER]", BBCodes.Hyperlink(member[0].ProfileLink, member[0].Name))
                .Replace("[TOP_CAMPAIGN_DAMAGE]", member[0].TopCampaignDamage.Damage.ToString("#,##0"))
                .Replace("[TOP_CAMPAIGN_DAMAGE_COUNTRY_IMG]", member[0].TopCampaignDamage.Country.Replace(' ', '_'))
                .Replace("[TOP_CAMPAIGN_DAMAGE_COUNTRY]", member[0].TopCampaignDamage.Country);

            Array.Sort<Citizen>(member, (x, y) => y.TruePatriotDamage.Damage.CompareTo(x.TruePatriotDamage.Damage));
            article = article
                .Replace("[TOP_TRUE_PATRIOT_PLAYER]", BBCodes.Hyperlink(member[0].ProfileLink, member[0].Name))
                .Replace("[TOP_TRUE_PATRIOT]", member[0].TruePatriotDamage.Damage.ToString("#,##0"))
                .Replace("[TOP_TRUE_PATRIOT_COUNTRY_IMG]", member[0].TruePatriotDamage.Country.Replace(' ', '_'))
                .Replace("[TOP_TRUE_PATRIOT_COUNTRY]", member[0].TruePatriotDamage.Country);

            Array.Sort<Citizen>(member, (x, y) => y.MonthlyDamage.CompareTo(x.MonthlyDamage));
            article = article
                .Replace("[TOP_MONTHLY_DAMAGE_PLAYER]", BBCodes.Hyperlink(member[0].ProfileLink, member[0].Name))
                .Replace("[TOP_MONTHLY_DAMAGE]", member[0].MonthlyDamage.ToString("#,##0"));

            Array.Sort<Citizen>(member, (x, y) => y.BombsUsed.Total.CompareTo(x.BombsUsed.Total));
            article = article
                .Replace("[TOP_BOMBS_USED_PLAYER]", BBCodes.Hyperlink(member[0].ProfileLink, member[0].Name))
                .Replace("[TOP_BOMBS_USED]", member[0].BombsUsed.Total.ToString());

            Array.Sort<Citizen>(member, (x, y) => y.Medals.BattleHero.CompareTo(x.Medals.BattleHero));
            article = article
                .Replace("[TOP_BATTLE_HERO_PLAYER]", BBCodes.Hyperlink(member[0].ProfileLink, member[0].Name))
                .Replace("[TOP_BATTLE_HERO]", member[0].Medals.BattleHero.ToString());

            Array.Sort<Citizen>(member, (x, y) => y.Medals.CampaignHero.CompareTo(x.Medals.CampaignHero));
            article = article
                .Replace("[TOP_CAMPAIGN_HERO_PLAYER]", BBCodes.Hyperlink(member[0].ProfileLink, member[0].Name))
                .Replace("[TOP_CAMPAIGN_HERO]", member[0].Medals.CampaignHero.ToString());

            Array.Sort<Citizen>(member, (x, y) => y.Medals.FreedomFighter.CompareTo(x.Medals.FreedomFighter));
            article = article
                .Replace("[TOP_FREEDOM_FIGHTER_PLAYER]", BBCodes.Hyperlink(member[0].ProfileLink, member[0].Name))
                .Replace("[TOP_FREEDOM_FIGHTER]", member[0].Medals.FreedomFighter.ToString());

            Array.Sort<Citizen>(member, (x, y) => y.Strength.CompareTo(x.Strength));
            article = article
                .Replace("[TOP_STRENGTH_PLAYER]", BBCodes.Hyperlink(member[0].ProfileLink, member[0].Name))
                .Replace("[TOP_STRENGTH]", member[0].Strength.ToString("#,##0"));

            Array.Sort<Citizen>(member, (x, y) => y.Medals.HardWorker.CompareTo(x.Medals.HardWorker));
            article = article
                .Replace("[TOP_HARD_WORKER_PLAYER]", BBCodes.Hyperlink(member[0].ProfileLink, member[0].Name))
                .Replace("[TOP_HARD_WORKER]", member[0].Medals.HardWorker.ToString());

            Array.Sort<Citizen>(member, (x, y) => y.Experience.CompareTo(x.Experience));
            article = article
                .Replace("[TOP_EXPERIENCE_PLAYER]", BBCodes.Hyperlink(member[0].ProfileLink, member[0].Name))
                .Replace("[TOP_EXPERIENCE_LEVEL]", member[0].Level.ToString())
                .Replace("[TOP_EXPERIENCE_POINTS]", member[0].Experience.ToString("#,##0"));

            Array.Sort<Citizen>(member, (x, y) => y.RankPoints.CompareTo(x.RankPoints));
            article = article
                .Replace("[TOP_RANK_PLAYER]", BBCodes.Hyperlink(member[0].ProfileLink, member[0].Name))
                .Replace("[TOP_RANK_LEVEL]", member[0].Rank)
                .Replace("[TOP_RANK_POINTS]", member[0].RankPoints.ToString("#,##0"));

            article = article
                .Replace("[DATE_SCAN]", scanDateBegin.ToString("dd.MM.yyy"))
                .Replace("[DATE_SCAN_BEGIN]", scanDateBegin.ToString("HH " + BBCodes.Superscript("mm")).Replace('.', '/'))
                .Replace("[DATE_SCAN_FINISH]", scanDateFinish.ToString("HH " + BBCodes.Superscript("mm")).Replace('.', '/'))
                .Replace("[MEMBERS_COUNT]", members.ToString())
                .Replace("[TOTAL_INFLUENCE]", infTotal.ToString("#,##0"))
                .Replace("[AVERAGE_INFLUENCE]", infAverage.ToString("#,##0"))
                .Replace("[FIGHTERS_PERCENTAGE]", actTotal.ToString() + "%")
                .Replace("[TOTAL_STRENGTH]", strTotal.ToString("#,##0"))
                .Replace("[AVERAGE_STRENGTH]", strAverage.ToString("#,##0"))
                .Replace("[AVERAGE_HIT]", hitAverage.ToString("#,##0"))
                .Replace("[AVERAGE_LEVEL]", lvlAverage.ToString())
                .Replace("[AVERAGE_RANK_NAME]", RankConstants[rnkAverage])
                .Replace("[NEW_MEDALS]", mdlNew.ToString("#,##0"))
                .Replace("[TOTAL_MEDALS]", mdlTotal.ToString("#,##0"))
                .Replace("[BOMBS_USED]", bmbTotal.ToString("#,##0"));

            article = article
                .Replace("[TOP_INFLUENCE_DIV1_STATISTICS]", GenerateTopInfluenceStatistics(1))
                .Replace("[TOP_INFLUENCE_DIV2_STATISTICS]", GenerateTopInfluenceStatistics(2))
                .Replace("[TOP_INFLUENCE_DIV3_STATISTICS]", GenerateTopInfluenceStatistics(3))
                .Replace("[TOP_INFLUENCE_DIV4_STATISTICS]", GenerateTopInfluenceStatistics(4))
                .Replace("[ACTIVITY_STATISTICS]", GenerateActivityStatistics())
                .Replace("[RANK_UP_STATISTICS]", GenerateRankUpStatistics())
                .Replace("[LEVEL_UP_STATISTICS]", GenerateLevelUpStatistics())
                .Replace("[NEW_MEDALS_STATISTICS]", GenerateNewMedalsStatistics());
            #endregion

            article += Environment.NewLine + BBCodes.Center(BBCodes.Hyperlink("http://www.erepublik.com/en/citizen/profile/2972052", BBCodes.Image("http://imageshack.com/a/img853/2286/pnql.png")));

            File.WriteAllText("Article.TXT", article);
        }
        private string GenerateTopInfluenceStatistics(int division)
        {
            string infStats = "";
            int ctzPos = 1;

            Array.Sort<Citizen>(member, (x, y) => y.Influence.CompareTo(x.Influence));

            for (int ctz = 0; ctz < members; ctz++)
                if (member[ctz].Influence > 0 && member[ctz].Division == division)
                {
                    infStats +=
                        BBCodes.Bold((ctzPos).ToString().PadLeft((int)Math.Floor(Math.Log10(members) + 1), '0') + ".) ") +
                        BBCodes.Hyperlink(member[ctz].ProfileLink, member[ctz].Name) + separator +
                        BBCodes.Bold("Inf: ") + member[ctz].Influence.ToString("#,##0") + separator +
                        BBCodes.Bold("Hit Q7: ") + member[ctz].Hit.ToString("#,##0") + separator +
                        BBCodes.Bold("Str: ") + member[ctz].Strength.ToString("#,##0") + separator +
                        BBCodes.Bold("Rank: ") + member[ctz].Rank + Environment.NewLine;

                    ctzPos += 1;
                }

            if (infStats == "")
                infStats = BBCodes.Image("http://www.britishproperties.org/images/none.png");

            return infStats;
        }
        private string GenerateActivityStatistics()
        {
            string actStats = "";
            int ctzPos = 1;

            Array.Sort<Citizen>(member, (x, y) => y.ExperienceGained.CompareTo(x.ExperienceGained));

            for (int ctz = 0; ctz < members; ctz++)
            {
                actStats +=
                    BBCodes.Bold((ctzPos).ToString().PadLeft((int)Math.Floor(Math.Log10(members) + 1), '0') + ".) ") +
                    BBCodes.Hyperlink(member[ctz].ProfileLink, member[ctz].Name) + separator +
                    BBCodes.Bold("Xp+: ") + member[ctz].ExperienceGained + separator +
                    BBCodes.Bold("Rank+: ") + member[ctz].RankPointsGained.ToString("#,##0") + separator +
                    BBCodes.Bold("Str+: ") + member[ctz].StrengthGained + separator +
                    BBCodes.Bold("Nat Rank: ") + member[ctz].NationalRank + separator +
                    BBCodes.Bold("Inf: ") + member[ctz].Influence.ToString("#,##0") + Environment.NewLine;

                ctzPos += 1;
            }

            return actStats;
        }
        private string GenerateRankUpStatistics()
        {
            string rnkStats = "";

            Citizen[] _member = member;
            Array.Sort<Citizen>(_member, (x, y) => y.RankValue.CompareTo(x.RankValue));

            for (int ctz = 0; ctz < members; ctz++)
                if (_member[ctz].RankUp != "")
                    rnkStats +=
                        BBCodes.Hyperlink(_member[ctz].ProfileLink, _member[ctz].Name) + " " +
                        BBCodes.Bold(_member[ctz].RankUp) + Environment.NewLine;

            if (rnkStats == "")
                rnkStats = BBCodes.Image("http://www.britishproperties.org/images/none.png");

            return rnkStats;
        }
        private string GenerateLevelUpStatistics()
        {
            string lvlStats = "";

            Citizen[] _member = member;
            Array.Sort<Citizen>(_member, (x, y) => y.Level.CompareTo(x.Level));

            for (int ctz = 0; ctz < members; ctz++)
                if (_member[ctz].LevelUp != "")
                    lvlStats +=
                        BBCodes.Hyperlink(_member[ctz].ProfileLink, _member[ctz].Name) + " " +
                        BBCodes.Bold(_member[ctz].LevelUp) + Environment.NewLine;

            if (lvlStats == "")
                lvlStats = BBCodes.Image("http://www.britishproperties.org/images/none.png");

            return lvlStats;
        }
        private string GenerateNewMedalsStatistics()
        {
            string[] mdlImage = new string[]
            {
                "https://imageshack.com/a/img822/9581/5k7h.png",
                "https://imageshack.com/a/img839/3491/vtx5.png",
                "https://imageshack.com/a/img594/8692/8yg.png",
                "https://imageshack.com/a/img560/3269/43of.png",
                "https://imageshack.com/a/img855/5469/pgi.png",
                "https://imageshack.com/a/img856/5008/goxh.png",
                "https://imageshack.com/a/img837/8571/4ni.png",
                "https://imageshack.com/a/img94/9393/d51y.png",
                "https://imageshack.com/a/img41/1508/khx0.png",
                "https://imageshack.com/a/img22/3031/f6q.png",
                "https://imageshack.com/a/img24/3580/c9ub.png",
                "https://imageshack.com/a/img153/2663/kcxi.png",
                "https://imageshack.com/a/img832/3143/jnwf.png",
            };
            string mdlStats = "";

            for (int i = 0; i < member[0].Medals.Medal.Length; i++)
            {
                Array.Sort<Citizen>(member, (x, y) => y.MedalsGained.Medal[i].CompareTo(x.MedalsGained.Medal[i]));

                string mdlCurrent = "";

                for (int ctz = 0; ctz < members; ctz++)
                    if (member[ctz].MedalsGained.Medal[i] > 0)
                        mdlCurrent +=
                            BBCodes.Bold(member[ctz].MedalsGained.Medal[i] + "x") + " " +
                            BBCodes.Hyperlink(member[ctz].ProfileLink, member[ctz].Name) +
                            Environment.NewLine;
            
                if(mdlCurrent != "")
                    mdlStats += BBCodes.Image(mdlImage[i]) + Environment.NewLine + mdlCurrent + Environment.NewLine;
            }

            return mdlStats;
        }
        #endregion
    }
}
