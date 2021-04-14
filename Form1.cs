using System;
using System.Windows.Forms;


namespace myspace
{
    public partial class Form1 : Form
    {
        static string scriptPath;
        public Form1()
        {
            InitializeComponent();
            folderTB.Text = "C:\\\\Users\\\\" + Environment.UserName + "\\\\Documents\\\\screenshots\\\\";
            dpTB.Text = "7";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tekla.Structures.TeklaStructuresSettings.GetAdvancedOption("XS_MACRO_DIRECTORY", ref scriptPath);
            if (scriptPath.IndexOf(';') > 0) { scriptPath = scriptPath.Remove(scriptPath.IndexOf(';')); }
            scriptPath += @"\modeling\";
            string DP = dpTB.Text; 
            string screenFolder = folderTB.Text;
            string filename = "+autosnaps.cs";
            bool keepDP=false;
            if(checkBox1.Checked) {keepDP=true;}
            Myclass.Dojob(filename, scriptPath, screenFolder, DP, keepDP);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
