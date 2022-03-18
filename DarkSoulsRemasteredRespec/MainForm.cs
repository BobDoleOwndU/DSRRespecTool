using System;
using System.Windows.Forms;

namespace DarkSoulsRemasteredRespec
{
    public partial class MainForm : Form
    {
        int[] stats = new int[8];
        int skillPoints;

        public MainForm()
        {
            InitializeComponent();
        }
        private void UpdateStat(int idx, int difference)
        {
            try
            {
                stats[idx] += difference;

                if(stats[idx] > 99 || stats[idx] < 1)
                {
                    stats[idx] -= difference;
                } //if
                else
                {
                    skillPoints -= difference;
                    skillPointTextBox.Text = skillPoints.ToString();

                    switch(idx)
                    {
                        case 0:
                            vitalityTextBox.Text = stats[idx].ToString();
                            break;
                        case 1:
                            attunementTextBox.Text = stats[idx].ToString();
                            break;
                        case 2:
                            enduranceTextBox.Text = stats[idx].ToString();
                            break;
                        case 3:
                            strengthTextBox.Text = stats[idx].ToString();
                            break;
                        case 4:
                            dexterityTextBox.Text = stats[idx].ToString();
                            break;
                        case 5:
                            resistanceTextBox.Text = stats[idx].ToString();
                            break;
                        case 6:
                            intelligenceTextBox.Text = stats[idx].ToString();
                            break;
                        case 7:
                            faithTextBox.Text = stats[idx].ToString();
                            break;
                    } //switch
                } //else
            } //try
            catch (Exception)
            {
                Console.WriteLine("Exception!");
            } //catch
        } //UpdateStat


        private void getStatsButton_Click(object sender, System.EventArgs e)
        {
            skillPoints = 0;
            stats = MemoryHandler.ReadMemory();

            int statsCount = stats.Length;

            vitalityTextBox.Text = stats[0].ToString();
            attunementTextBox.Text = stats[1].ToString();
            enduranceTextBox.Text = stats[2].ToString();
            strengthTextBox.Text = stats[3].ToString();
            dexterityTextBox.Text = stats[4].ToString();
            resistanceTextBox.Text = stats[5].ToString();
            intelligenceTextBox.Text = stats[6].ToString();
            faithTextBox.Text = stats[7].ToString();
            skillPointTextBox.Text = skillPoints.ToString();
        } //UpdateStatsButton_Click

        private void updateStatsButton_Click(object sender, EventArgs e)
        {
            if(skillPoints != 0)
            {
                MessageBox.Show("Skill points not at 0!");
            } //if
            else
            {
                MemoryHandler.WriteMemory(stats);
            } //else
        } //updateStatsButton_Click

        private void incVitalityButton_Click(object sender, System.EventArgs e)
        {
            UpdateStat(0, 1);
        } //incVitalityButton_Click

        private void decVitalityButton_Click(object sender, System.EventArgs e)
        {
            UpdateStat(0, -1);
        } //incVitalityButton_Click

        private void incAttunementButton_Click(object sender, EventArgs e)
        {
            UpdateStat(1, 1);
        }

        private void decAttunementButton_Click(object sender, EventArgs e)
        {
            UpdateStat(1, -1);
        }

        private void incEnduranceButton_Click(object sender, EventArgs e)
        {
            UpdateStat(2, 1);
        }

        private void decEnduranceButton_Click(object sender, EventArgs e)
        {
            UpdateStat(2, -1);
        }

        private void incStrengthButton_Click(object sender, EventArgs e)
        {
            UpdateStat(3, 1);
        }

        private void decStrengthButton_Click(object sender, EventArgs e)
        {
            UpdateStat(3, -1);
        }

        private void incDexterityButton_Click(object sender, EventArgs e)
        {
            UpdateStat(4, 1);
        }

        private void decDexterityButton_Click(object sender, EventArgs e)
        {
            UpdateStat(4, -1);
        }

        private void incResistanceButton_Click(object sender, EventArgs e)
        {
            UpdateStat(5, 1);
        }

        private void decResistanceButton_Click(object sender, EventArgs e)
        {
            UpdateStat(5, -1);
        }

        private void incIntelligenceButton_Click(object sender, EventArgs e)
        {
            UpdateStat(6, 1);
        }

        private void decIntelligenceButton_Click(object sender, EventArgs e)
        {
            UpdateStat(6, -1);
        }

        private void incFaithButton_Click(object sender, EventArgs e)
        {
            UpdateStat(7, 1);
        }

        private void decFaithButton_Click(object sender, EventArgs e)
        {
            UpdateStat(7, -1);
        }
    } //class
} //namespace
