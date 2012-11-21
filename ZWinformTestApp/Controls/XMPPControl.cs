using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibHipChat.XMPP;
using ZWinformTestApp.Services;
using agsXMPP.Xml.Dom;

namespace ZWinformTestApp.Controls
{     
    public partial class XMPPControl : UserControl
    {
        private HipChatXMPPConnection _xmppConnection;

        private delegate void SetTextCallback(string text);
        public XMPPControl()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _xmppConnection = new HipChatXMPPConnection(new XMPPConnectionSettingsProvider().GetConnectionSettings());
                SetupEvents();
            }
            

        }

        private void bnConnect_Click(object sender, EventArgs e)
        {
            _xmppConnection.OpenConnection();            
        }

        public void SetupEvents()
        {
            _xmppConnection.ClientConnection.OnLogin += delegate { SetMessageText("On Login Called"); };
            _xmppConnection.ClientConnection.OnError += delegate(object sender, Exception exception)
                                                            {
                                                                SetMessageText(String.Format("On Error Called: {0}",                                                                                               exception.Message));
                                                            };
            _xmppConnection.ClientConnection.OnAuthError += delegate(object sender, Element element)
                                                                {
                                                                    SetMessageText(
                                                                        String.Format("On Auth Error Called: {0}",
                                                                                      element.ToString()));
                                                                };

        }

        private void bnDisconnect_Click(object sender, EventArgs e)
        {
            _xmppConnection.CloseConnection();
        }

        private void SetMessageText(string newText)
        {
            if (this.tbOutput.InvokeRequired)
            {
                SetTextCallback d = SetMessageText;
                Invoke(d, newText);
            }
            else
            {
                tbOutput.Text += newText + Environment.NewLine;
            }
        }
    }
}
