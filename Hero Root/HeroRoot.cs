using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using RegawMOD.Android;

namespace Hero_Root
{
    public partial class RootForm : Form
    {
        #region CONSTANTS
        // Constant Fields \\
        private const string NEW_LINE = "\r\n";
        private const string ANDROID_DRIVER_PATH = "\\drivers\\ANDROIDUSB.sys";
        private const string EVO_DEVICE_NAME = "supersonic";
        private const string HEROC_DEVICE_NAME = "heroc";
        private const string RO_PRODUCT_DEVICE = "ro.product.device";

        // Global Fields \\
        private bool hasRoot = false;
        private bool canQuit = true;

        // Static Fields \\
        private static readonly string DEBUG_PATH = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\HeroRoot.log";
        private static string[] exploits = { "rageagainstthecage-arm5.bin", "recovery.img", "su", "Superuser.apk", "flash_image", "boot.img", "s-off.zip" };
        private static string tempDir = Path.GetTempPath() + "HeroRoot\\";

        // Message Box Dialogues \\
        private const string MISSING_DRIVER = "Necessary HTC drivers are missing.\r\nPlease get drivers by installing HTC Sync, or directly with the driver zip (advanced)";
        private const string MISSING_DRIVER_CAP = "Missing HTC Drivers";
        private const string DO_NOT_UNPLUG = "RegawMOD Hero Rooter is about to start.\nDo NOT unplug your Hero from the computer until promted!";
        private const string WARNING = "About To Start!";
        private const string UNPLUG = "Rooting is now complete!\nYou may now unplug your Hero!";
        private const string UNPLUG_CAP = "Rooting Complete!";

        #endregion

        public RootForm()
        {
            InitializeComponent();
        }

        private AndroidController android;
        private Device device;
        AdbCommand adbCmd;

        private void Debug(params string[] toWrite)
        {
            for (int i = 0; i < toWrite.Length; i++)
                using (StreamWriter w = new StreamWriter(DEBUG_PATH, true))
                    w.WriteLine(toWrite[i]);
        }

        private void RootForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + ANDROID_DRIVER_PATH) &&
                !File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86) + ANDROID_DRIVER_PATH))
            {
                MessageBox.Show(MISSING_DRIVER, MISSING_DRIVER_CAP, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            
            ExtractResources();
        }

        private void RootForm_Shown(object sender, EventArgs e)
        {
            CheckFor_();
            txtRootOut.AppendText("Starting adb server..." + NEW_LINE);

            android = new AndroidController();

            txtRootOut.Text = txtRootOut.Text.Replace("Starting adb server..." + NEW_LINE, "");

            timerCursor.Enabled = true;
            timerPhone.Enabled = true;

            timerCursor.Start();
            timerPhone.Start();
        }

        private void RootForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canQuit)
                e.Cancel = true;
        }

        private void RootForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (android != null)
                android.Dispose();

            Thread.Sleep(1000);

            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }

        private void btnRoot_Click(object sender, EventArgs e)
        {
            canQuit = false;
            timerPhone.Enabled = false;
            btnRoot.Visible = false;

            MessageBox.Show(DO_NOT_UNPLUG, WARNING, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            lblPhoneConnect.Text += " - ROOTING";

            CheckFor_();
            txtRootOut.AppendText("Pushing Exploit to Phone..." + NEW_LINE);
            
            adbCmd = Adb.FormAdbCommand(this.device, "push", tempDir + "rageagainstthecage-arm5.bin", @"/data/local/tmp");
            Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);

            adbCmd = Adb.FormAdbShellCommand(this.device, false, "chmod", "755", "/data/local/tmp/rageagainstthecage-arm5.bin");
            Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);
            
            CheckFor_();
            txtRootOut.AppendText("\tPushed.");

            NewConLine();
            CheckFor_();
            txtRootOut.AppendText("Running Exploit...");

            int counter = 1;

            while (!hasRoot)
            {
                if (counter++ == 3)
                    break;

                Adb.ExecuteAdbShellCommandInputString(this.android, this.device, "./data/local/tmp/rageagainstthecage-arm5.bin");

                NewConLine();
                CheckFor_();
                txtRootOut.AppendText("Waiting For Reconnect..." + NEW_LINE);

                Thread.Sleep(20000);//Tempfix, werks for nows

                adbCmd = Adb.FormAdbShellCommand(this.device, false, "id");
                string uid = Adb.ExecuteAdbCommand(this.android, adbCmd);

                if (uid.Contains("uid=0"))
                    hasRoot = true;
                else
                    txtRootOut.Text = txtRootOut.Text.Replace(NEW_LINE + NEW_LINE + ">" + "Waiting For Reconnect..." + NEW_LINE, "");
            }

            if (!hasRoot)
            {
                MessageBox.Show("RegawMOD Hero Rooter failed to get root this time!\nRestart your Hero, and run this rooter again", "Root Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug(string.Format("Exploit In TempDir? - {0}", File.Exists(tempDir + "rageagainstthecage-arm5.bin")));
                device.Reboot();
                canQuit = true;
                this.Close();
                Application.Exit();
                return;
            }

            CheckFor_();
            txtRootOut.AppendText("\tRoot Achieved.");

            adbCmd = Adb.FormAdbShellCommand(this.device, false, "mount", "-o remount,rw", "-t yaffs2", "/dev/block/mtdblock3", "/system"); //Special Case, dont use FileSystem.RemountSystem(RW), doesn't have SU bin yet...
            Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);

            if (device.FileSystem.SystemMountInfo.MountType != MountType.RW)
            {
                MessageBox.Show("Error Mounting File System as RW\nRestart Rooter after device reboots", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                device.Reboot();
                canQuit = true;
                this.Close();
                Application.Exit();
                return;
            }

            NewConLine();
            CheckFor_();
            txtRootOut.AppendText("Pushing Other Root Files..." + NEW_LINE);

            adbCmd = Adb.FormAdbCommand(this.device, "push", tempDir + "su", @"/system/xbin");
            Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);

            adbCmd = Adb.FormAdbShellCommand(this.device, false, "chmod", "4755", @"/system/xbin/su");
            Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);

            adbCmd = Adb.FormAdbCommand(this.device, "push", tempDir + "Superuser.apk", @"/system/app");
            Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);
            
            adbCmd = Adb.FormAdbCommand(this.device, "push", tempDir + "flash_image", @"/system/xbin");
            Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);

            adbCmd = Adb.FormAdbShellCommand(this.device, false, "chmod", "755", @"/system/xbin/flash_image");
            Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);

            adbCmd = Adb.FormAdbCommand(this.device, "push", tempDir + "recovery.img", @"/sdcard/");
            Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);

            adbCmd = Adb.FormAdbCommand(this.device, "push", tempDir + "boot.img", @"/sdcard/");
            Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);

            adbCmd = Adb.FormAdbCommand(this.device, "push", tempDir + "s-off.zip", @"/sdcard/");
            Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);

            CheckFor_();
            txtRootOut.AppendText("\tPushed.");

            NewConLine();
            CheckFor_();
            txtRootOut.AppendText("Flashing Recovery/Boot..." + NEW_LINE);
            
            // Built in Check to make sure we're flashing to a Heroc, nothing else
            if (device.BuildProp.GetProp(RO_PRODUCT_DEVICE) == HEROC_DEVICE_NAME)
            {
                adbCmd = Adb.FormAdbShellCommand(this.device, false, "flash_image", "recovery", @"/sdcard/recovery.img");
                Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);
                adbCmd = Adb.FormAdbShellCommand(this.device, false, "flash_image", "boot", @"/sdcard/boot.img");
                Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);
            }

            CheckFor_();
            txtRootOut.AppendText("\tRecovery/Boot Flashed.");

            adbCmd = Adb.FormAdbShellCommand(this.device, false, "rm", @"/data/local/tmp/rageagainstthecage-arm5.bin");
            Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);
            adbCmd = Adb.FormAdbShellCommand(this.device, false, "rm", @"/sdcard/recovery.img");
            Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);
            adbCmd = Adb.FormAdbShellCommand(this.device, false, "rm", @"/sdcard/boot.img");
            Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);
            
            NewConLine();
            CheckFor_();
            txtRootOut.AppendText("Sending Commands To Recovery..." + NEW_LINE);

            adbCmd = Adb.FormAdbShellCommand(this.device, false, "echo", "\'boot-recovery\' > /cache/recovery/command");
            Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);

            adbCmd = Adb.FormAdbShellCommand(this.device, false, "echo", "\'--update_package=/sdcard/s-off.zip\' >> /cache/recovery/command");
            Adb.ExecuteAdbCommandNoReturn(this.android, adbCmd);

            CheckFor_();
            txtRootOut.AppendText("\tSent.");

            NewConLine();
            CheckFor_();
            txtRootOut.AppendText("Rebooting to Flash S-Off..." + NEW_LINE);

            device.RebootRecovery();

            CheckFor_();
            txtRootOut.AppendText("\tRooting Complete.");

            lblPhoneConnect.Text = lblPhoneConnect.Text.Replace("Connected - ROOTING", "ROOTED");
            canQuit = true;

            MessageBox.Show(UNPLUG, UNPLUG_CAP, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void txtRootOut_Enter(object sender, EventArgs e)
        {
            lblFocus.Focus();
        }

        private void CheckFor_()
        {
            if (txtRootOut.Text.LastIndexOf('_') == txtRootOut.Text.Length - 1)
                txtRootOut.Text = txtRootOut.Text.Remove(txtRootOut.Text.LastIndexOf('_'));
        }

        private void NewConLine()
        {
            txtRootOut.AppendText(NEW_LINE + NEW_LINE + ">_");
        }

        #region EXTRACT
        private void ExtractResources()
        {
            Assembly rooter = Assembly.GetExecutingAssembly();

            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);

            Directory.CreateDirectory(tempDir);

            for (int i = 0; i != exploits.Length; i++)
                using (Stream resToGet = rooter.GetManifestResourceStream("Hero_Root.Extract.Exploits." + exploits[i]))
                    using (BinaryReader br = new BinaryReader(resToGet))
                        using (FileStream fs = new FileStream(tempDir + exploits[i], FileMode.OpenOrCreate))
                            using (BinaryWriter bw = new BinaryWriter(fs))
                                bw.Write(br.ReadBytes((int)resToGet.Length));
        }
        #endregion

        #region TIMERS
        private void timerCursor_Tick(object sender, EventArgs e)
        {
            if (txtRootOut.Text.LastIndexOf('_') == txtRootOut.Text.Length - 1)
                txtRootOut.Text = txtRootOut.Text.Remove(txtRootOut.Text.LastIndexOf('_'));
            else
                txtRootOut.AppendText("_");
        }

        private void timerPhone_Tick(object sender, EventArgs e)
        {
            android.UpdateDeviceList();

            if (!android.HasConnectedDevices)
            {
                device = null;
                lblPhoneConnect.Text = "Phone Not Connected";
                btnRoot.Visible = false;
            }
            else if (device != null && device.SerialNumber == android.ConnectedDevices[0])
            {
                return;
            }
            else 
            {
                device = android.GetConnectedDevice();
                lblPhoneConnect.Text = device.SerialNumber + " Connected";

                if (!device.BuildProp.GetProp(RO_PRODUCT_DEVICE).Contains(HEROC_DEVICE_NAME))
                {
                    lblPhoneConnect.Text += " - NOT A CDMA Hero";
                    return;
                }

                if (!device.HasRoot)
                {
                    btnRoot.Visible = true;
                }
                else
                {
                    lblPhoneConnect.Text = device.SerialNumber + " Connected - Already Rooted!";
                }
            }
        }
        #endregion
    }
}