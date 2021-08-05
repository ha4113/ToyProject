using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace TestServer
{
    public partial class ServerForm : Form
    {
        private ConnectionManager _connectionMgr { get; set; }

        public ServerForm()
        {
            InitializeComponent();

            LogOut.Visible = false;
            Log("Hello");
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            var userId = TextBoxID.Text;
            var userPw = TextBoxPW.Text;

            var connectionString = $"server={ServerConst.HOST};port={ServerConst.PORT};Database={ServerConst.DATABASE};User ID={userId};Password={userPw}";
            DatabaseConnect(connectionString);
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            if (_connectionMgr != null &&
                _connectionMgr.State == ConnectionState.Open)
            {
                _connectionMgr.Close();
                SetConnection(false);
                return;
            }

            Close();
        }

        private void SetConnection(bool isConnect)
        {
            Connect.Enabled = !isConnect;
            LogOut.Visible = isConnect;
            TextBoxID.ReadOnly = !isConnect;
            TextBoxPW.ReadOnly = !isConnect;
        }

        private void DatabaseConnect(string connectionString)
        {
            Log("Try Connection...", LogType.HIGHLIGHT);
            Log($"ConnectionStr : {connectionString}");

            try
            {
                if (_connectionMgr == null)
                {
                    _connectionMgr = new ConnectionManager(Log);
                }

                _connectionMgr.Connect(connectionString);

                Properties.Settings.Default.username = TextBoxID.Text;
                DialogResult = DialogResult.OK; 
                
                SetConnection(true);
            }
            catch (Exception e)
            {
                Log(e.ToString(), LogType.ERROR);
                SetConnection(false);
            }
        }

        public void Log(string log, LogType logType = LogType.NONE)
        {
            Color color;

            switch (logType)
            {
            case LogType.WARNING: { color = Color.Yellow; } break;
            case LogType.ERROR: { color = Color.Red; } break;
            case LogType.HIGHLIGHT: { color = Color.Aquamarine; } break;
            default: { color = Color.White; } break;
            }

            TextConsole.SelectionColor = color;

            TextConsole.AppendText(log);
            TextConsole.AppendText("\n");
            TextConsole.ScrollToCaret();
        }
    }
}
