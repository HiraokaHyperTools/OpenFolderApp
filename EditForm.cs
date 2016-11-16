using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OpenFolderApp {
    public partial class EditForm : Form {
        public EditForm(String fpxml) {
            InitializeComponent();

            if (File.Exists(this.fpxml = fpxml)) {
                using (FileStream fs = File.OpenRead(fpxml)) {
                    if (fs.Length > 0) {
                        ofa = (OFA)xs.Deserialize(fs);
                    }
                }
            }
            ofa = ofa ?? new OFA();

            tbDir.DataBindings.Add("Text", ofa, "filePath");
        }

        XmlSerializer xs = new XmlSerializer(typeof(OFA));

        String fpxml;
        OFA ofa;

        private void bRefDir_Click(object sender, EventArgs e) {
            ValidateChildren();

            fbdDir.SelectedPath = tbDir.Text;
            if (fbdDir.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) {
                tbDir.Text = fbdDir.SelectedPath;
            }
        }

        private void fbdDir_HelpRequest(object sender, EventArgs e) {
        }

        private void bSave_Click(object sender, EventArgs e) {
            ValidateChildren();

            using (FileStream fs = File.Create(fpxml)) {
                xs.Serialize(fs, ofa);
            }
            MessageBox.Show(this, "保存しました。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    [XmlRoot(Namespace = "https://github.com/HiraokaHyperTools/OpenFolderApp/1")]
    public class OFA {
        [XmlAttribute()]
        public String filePath { get; set; }
    }
}
