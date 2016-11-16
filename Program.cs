using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OpenFolderApp {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length >= 2 && args[0] == "/o") {
                XmlSerializer xs = new XmlSerializer(typeof(OFA));
                using (FileStream fs = File.OpenRead(args[1])) {
                    if (fs.Length > 0) {
                        var ofa = (OFA)xs.Deserialize(fs);
                        var fp = ofa.filePath;

                        Match M = Regex.Match(fp, "^\\\\\\\\[^\\\\]+\\\\", RegexOptions.IgnoreCase);
                        if (M.Success) {
                            String wantTo = M.Value;
                            if (GetFileAttributes(fp) == uint.MaxValue) {
                                int err = Marshal.GetLastWin32Error();
                                if (err == 5) {
                                    IntPtr ptr;
                                    int nRead, nTotal;
                                    int r = NetUseEnum(null, 2, out ptr, 65536, out nRead, out nTotal, IntPtr.Zero);
                                    if (r == 0) {
                                        USE_INFO_2[] ents = new USE_INFO_2[nRead];
                                        String conns = null;
                                        List<USE_INFO_2> targets = new List<USE_INFO_2>();
                                        for (int x = 0; x < ents.Length; x++) {
                                            var ent = ents[x] = (USE_INFO_2)Marshal.PtrToStructure(new IntPtr(ptr.ToInt64() + Marshal.SizeOf(typeof(USE_INFO_2)) * x), typeof(USE_INFO_2));
                                            if (ent.remote.IndexOf(wantTo, StringComparison.InvariantCultureIgnoreCase) == 0) {
                                                conns += (ent.local + " " + ent.remote + " (" + ent.domainname + "\\" + ent.username + ")").Trim() + "\n";
                                                targets.Add(ent);
                                            }
                                        }
                                        if (conns != null) {
                                            DialogResult selection = MessageBox.Show(String.Format("{0} へアクセスできません。次の接続をすべて強制的に切断して、アクセスしますか。\n\n" + conns, fp), "OpenFolderApp", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                                            if (selection == DialogResult.Yes) {
                                                foreach (USE_INFO_2 ent in ents) {
                                                    if (ent.local != null) {
                                                        NetUseDel(null, ent.local, USE_LOTS_OF_FORCE);
                                                    }
                                                    if (ent.remote != null) {
                                                        NetUseDel(null, ent.remote, USE_LOTS_OF_FORCE);
                                                    }
                                                }
                                            }
                                            else if (selection == DialogResult.Cancel) {
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        try {
                            Process.Start(ofa.filePath);
                        }
                        catch (Exception err) {
                            MessageBox.Show(err.Message, "OpenFolderApp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                }
            }
            else if (args.Length >= 2 && args[0] == "/e") {
                Application.Run(new EditForm(args[1]));
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint GetFileAttributes(string lpFileName);

        [DllImport("NetApi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int NetUseEnum(
            String UncServerName,
            int Level,
            out IntPtr Buf,
            int PreferedMaximumSize,
            out int EntriesRead,
            out int TotalEntries,
            IntPtr resumeHandle);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct USE_INFO_2 {
            internal String local;
            internal String remote;
            internal String password;
            internal int status;
            internal int asgType;
            internal int refcount;
            internal int usecount;
            internal String username;
            internal String domainname;

            public override string ToString() {
                return String.Format("{0}\\{1}", domainname, username);
            }
        }

        const UInt32 USE_NOFORCE = 0;
        const UInt32 USE_FORCE = 1;
        const UInt32 USE_LOTS_OF_FORCE = 2;

        [DllImport("NetApi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern UInt32 NetUseDel(
            string UncServerName,
            string UseName,
            UInt32 ForceCond
        );
    }
}
