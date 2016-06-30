using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net;
using ARSoft.Tools.Net;
using ARSoft.Tools.Net.Dns;
using Newtonsoft.Json;

namespace DomainInfo
{
    public partial class Form1 : Form
    {
        private string domain;

        public Form1()
        {
            InitializeComponent();
        }

        

        private void txtDomain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RunAllChecks();
            }
        }

        private void RunAllChecks() //runs all checks --------------------------------------
        {
            //clear current boxes
            lstDNS.Items.Clear();
            lstDNSData.Items.Clear();
            lstDNSRecord.Items.Clear();
            lstDNSTtl.Items.Clear();
            lstWhois.Items.Clear();
            lstPropagation.Items.Clear();

            //update statuses
            lblStatusWhois.Text = "Checking WHOIS...";
            SetStatusDNS("Checking DNS...");
            SetStatusProp("Checking Propagation...");
            lblStatusWhois.Visible = true;
            lblStatusDNS.Visible = true;
            lblStatusProp.Visible = true;

            //set the global domain variable to be the one we want to check
            if (txtDomain.Text.Contains("www."))
            {
                domain = txtDomain.Text.Replace("www.", "");
                txtDomain.Text = domain;
            }
            if (txtDomain.Text.Contains("https"))
            {
                domain = txtDomain.Text.Replace("https", "");
                txtDomain.Text = domain;
            }
            if (txtDomain.Text.Contains("http"))
            {
                domain = txtDomain.Text.Replace("http", "");
                txtDomain.Text = domain;
            }
            if (txtDomain.Text.Contains("/"))
            {
                domain = txtDomain.Text.Replace("/", "");
                txtDomain.Text = domain;
            }
            if (txtDomain.Text.Contains(":"))
            {
                domain = txtDomain.Text.Replace(":", "");
                txtDomain.Text = domain;
            }
            if (txtDomain.Text.Contains(" "))
            {
                domain = txtDomain.Text.Replace(" ", "");
                txtDomain.Text = domain;
            }
            if (txtDomain.Text.Contains("\t"))
            {
                domain = txtDomain.Text.Replace("\t", "");
                txtDomain.Text = domain;
            }
            else
            {
                domain = txtDomain.Text.ToString();
            }

            //begin checking functions
            findWhois();
            findDNS();
            findProp();
        }


        //START WHOIS CODE------------------------------------------------------
        private void findWhois()
        {
            BackgroundWorker bwWhois = new BackgroundWorker();
            bwWhois.WorkerReportsProgress = false;
            bwWhois.WorkerSupportsCancellation = false;
            bwWhois.DoWork += new DoWorkEventHandler(bwWhois_DoWork);
            bwWhois.RunWorkerAsync();
        }//create the background worker and initialize it

        private void bwWhois_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            var whois = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "whois.exe",
                    Arguments = domain,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            whois.Start();

            while (!whois.StandardOutput.EndOfStream)
            {
                string line = whois.StandardOutput.ReadLine();
                if (line != "")
                {
                    LogWhois(line);
                }
                
                SetStatusWhois("WHOIS complete");
            }
            
        }//execute actual operation code to get the WHOIS info
        //END WHOIS CODE -------------------------------------------------------
        //START DNS CODE--------------------------------------------------------
        private void findDNS()
        {
            //clear previous results
            strTXTrecs = null;
            strCNAMErecs = null;
            strArecs = null;
            strMXrecs = null;
            strNSrecs = null;

            BackgroundWorker bwDNS = new BackgroundWorker();
            bwDNS.WorkerReportsProgress = false;
            bwDNS.WorkerSupportsCancellation = false;
            bwDNS.DoWork += new DoWorkEventHandler(bwDNS_DoWork);
            bwDNS.RunWorkerAsync();
        } //create the background worker and initialize it

        //create placeholders for the text report we make later on
        string[] strArecs;
        string[] strMXrecs;
        string[] strNSrecs;
        string[] strTXTrecs;
        string[] strCNAMErecs;

        //WORM CAN
        private void bwDNS_DoWork(object sender, DoWorkEventArgs e)
        {
            strArecs = new string[20];
            strMXrecs = new string[20];
            strNSrecs = new string[20];
            strTXTrecs = new string[20];
            strCNAMErecs = new string[20];

            //A records
            try
            {
                //get a response from the resolver
                DnsMessage responseA = DnsClient.Default.Resolve(DomainName.Parse(domain), RecordType.A);
                if (responseA.AnswerRecords.Count >= 1) //make sure there actually is an answer
                {
                    IEnumerable<ARecord> Arecords = responseA.AnswerRecords.OfType<ARecord>(); //isolate A record results and make a list of them, even if it's just one
                    int x = 0;
                    foreach (ARecord a in Arecords)//throw all results to the GUI
                    {
                        LogDNSDomain(a.Name.ToString());
                        LogDNSRecord(a.RecordType.ToString());
                        LogDNSData(a.Address.ToString());
                        LogDNSTTL(a.TimeToLive.ToString());

                        strArecs[x] = a.Address.ToString();
                        x++;
                    }
                }
                else //no results found
                {
                    LogDNSDomain("No A records available");
                    LogDNSRecord(" ");
                    LogDNSData(" ");
                    LogDNSTTL(" ");
                    strArecs = null;
                }
            }
            catch (NullReferenceException)
            {
                LogDNSDomain("No A records available");
                LogDNSRecord(" ");
                LogDNSData(" ");
                LogDNSTTL(" ");
                strArecs = null;
            }

            //MX records
            try
            {
                DnsMessage responseMX = DnsClient.Default.Resolve(DomainName.Parse(domain), RecordType.Mx);
                if (responseMX.AnswerRecords.Count >= 1)
                {
                    IEnumerable<MxRecord> Mxrecords = responseMX.AnswerRecords.OfType<MxRecord>();
                    int x = 0;
                    foreach (MxRecord a in Mxrecords) //throw all results to the GUI
                    {
                        if (a.ExchangeDomainName.ToString().Contains("mail."))//if the domain has a "mail.domain.com" MX record, go get the mail.domain.com A record. 
                        {
                            DnsMessage responseA = DnsClient.Default.Resolve(DomainName.Parse("mail." + domain), RecordType.A);
                            if (responseA.AnswerRecords.Count >= 1) //make sure there actually is an answer
                            {
                                IEnumerable<ARecord> Arecords = responseA.AnswerRecords.OfType<ARecord>(); //isolate A record results and make a list of them, even if it's just one
                                int f = 0;
                                foreach (ARecord y in Arecords)//throw all results to the GUI
                                {
                                    LogDNSDomain(y.Name.ToString());
                                    LogDNSRecord(y.RecordType.ToString());
                                    LogDNSData(y.Address.ToString());
                                    LogDNSTTL(y.TimeToLive.ToString());

                                    strArecs[x] = y.Address.ToString();
                                    f++;
                                }
                            }
                        }
                        LogDNSDomain(a.Name.ToString());
                        LogDNSRecord(a.RecordType.ToString());
                        LogDNSData(a.Preference.ToString() + "   " + a.ExchangeDomainName.ToString());
                        LogDNSTTL(a.TimeToLive.ToString());

                        strMXrecs[x] = a.Preference.ToString() + "   " + a.ExchangeDomainName.ToString();
                        x++;
                    }
                }
                else //no results found
                {
                    LogDNSDomain("No MX records available");
                    LogDNSRecord(" ");
                    LogDNSData(" ");
                    LogDNSTTL(" ");
                    strMXrecs = null;
                }
            }
            catch (NullReferenceException)
            {
                LogDNSDomain("No MX records available");
                LogDNSRecord(" ");
                LogDNSData(" ");
                LogDNSTTL(" ");
                strMXrecs = null;
            }

            //CNAME records
            try
            {
                DnsMessage responseCname = DnsClient.Default.Resolve(DomainName.Parse(domain), RecordType.CName);
                if (responseCname.AnswerRecords.Count >= 1)
                {
                    IEnumerable<CNameRecord> Txtrecords = responseCname.AnswerRecords.OfType<CNameRecord>();
                    int x = 0;
                    foreach (CNameRecord a in Txtrecords)//throw all results to the GUI
                    {
                        LogDNSDomain(a.Name.ToString());
                        LogDNSRecord(a.RecordType.ToString());
                        LogDNSData(a.CanonicalName.ToString());
                        LogDNSTTL(a.TimeToLive.ToString());

                        strCNAMErecs[x] = a.CanonicalName.ToString();
                        x++;
                    }
                }
                else //no results found
                {
                    LogDNSDomain("No CNAME records available");
                    LogDNSRecord(" ");
                    LogDNSData(" ");
                    LogDNSTTL(" ");
                    strCNAMErecs = null;
                }
            }
            catch (NullReferenceException)
            {
                LogDNSDomain("No CNAME records available");
                LogDNSRecord(" ");
                LogDNSData(" ");
                LogDNSTTL(" ");
                strCNAMErecs = null;
            }

            //TXT records
            try
            {
                DnsMessage responseTxt = DnsClient.Default.Resolve(DomainName.Parse(domain), RecordType.Txt);
                if (responseTxt.AnswerRecords.Count >= 1)
                {
                    IEnumerable<TxtRecord> Txtrecords = responseTxt.AnswerRecords.OfType<TxtRecord>();
                    int x = 0;
                    foreach (TxtRecord a in Txtrecords)//throw all results to the GUI
                    {
                        LogDNSDomain(a.Name.ToString());
                        LogDNSRecord(a.RecordType.ToString());
                        LogDNSData(a.TextData.ToString());
                        LogDNSTTL(a.TimeToLive.ToString());

                        strTXTrecs[x] = a.TextData.ToString();
                    }
                }
                else //no results found
                {
                    LogDNSDomain("No TXT records available");
                    LogDNSRecord(" ");
                    LogDNSData(" ");
                    LogDNSTTL(" ");
                    strTXTrecs = null;
                }
            }
            catch (NullReferenceException)
            {
                LogDNSDomain("No TXT records available");
                LogDNSRecord(" ");
                LogDNSData(" ");
                LogDNSTTL(" ");
                strTXTrecs = null;
            }

            //NS records
            try
            {
                DnsMessage responseNS = DnsClient.Default.Resolve(DomainName.Parse(domain), RecordType.Ns);
                if (responseNS.AnswerRecords.Count >= 1)
                {
                    IEnumerable<NsRecord> NSrecords = responseNS.AnswerRecords.OfType<NsRecord>();
                    int x = 0;
                    foreach (NsRecord a in NSrecords)//throw all results to the GUI
                    {
                        LogDNSDomain(a.Name.ToString());
                        LogDNSRecord(a.RecordType.ToString());
                        LogDNSData(a.NameServer.ToString());
                        LogDNSTTL(a.TimeToLive.ToString());

                        strNSrecs[x] = a.NameServer.ToString();
                    }
                }
                else //no results found
                {
                    LogDNSDomain("No NS records available");
                    LogDNSRecord(" ");
                    LogDNSData(" ");
                    LogDNSTTL(" ");
                    strNSrecs = null;
                }
            }
            catch (NullReferenceException)
            {
                LogDNSDomain("No NS records available");
                LogDNSRecord(" ");
                LogDNSData(" ");
                LogDNSTTL(" ");
                strNSrecs = null;
            }


            SetStatusDNS("Complete");
        }//execute actual operation code to get the DNS info
        //END WORM CAN

        //END DNS CODE----------------------------------------------------------
        //START PROP CODE-------------------------------------------------------
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbPropType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lstPropagation.Items.Clear();

            findProp();
        }//when the type of record we're looking for changes, update the results

        string rec; //placeholder for the type of record to check for propagation
        bool res = false; //did the record resolve?
        private void findProp()
        {
            rec = cmbPropType.Text; //get this variable now so we can call it safely from the worker
            BackgroundWorker bwProp = new BackgroundWorker();
            bwProp.WorkerReportsProgress = false;
            bwProp.WorkerSupportsCancellation = false;
            bwProp.DoWork += new DoWorkEventHandler(bwProp_DoWork);
            bwProp.RunWorkerAsync();
        }//create the background worker and initialize it

        private void bwProp_DoWork(object sender, DoWorkEventArgs e)
        {
            //create string array for the nodes
            string strNodes;
            using (WebClient cli = new WebClient())
            {
                //get the list of nodes from the API
                strNodes = cli.DownloadString("http://www.dns-lg.com/nodes.json");
                cli.Dispose();
            }

            //deserialize the data into a class that we can work with
            RootObject nodesList = JsonConvert.DeserializeObject<RootObject>(strNodes);
            
            foreach (Node n in nodesList.nodes)
            {
                res = false;
                try
                {
                    //get the result for the specified record type
                    using (WebClient cli = new WebClient())
                    {
                        string url = "http://www.dns-lg.com/" + n.name.ToString() + "/" + domain + "/" + rec;
                        string json = cli.DownloadString(url);
                        RootObject result = JsonConvert.DeserializeObject<RootObject>(json);
                        if (result.answer != null)
                        {
                            if (result.answer[0] != null)
                            {
                                //LogProp(result.answer[0].rdata.ToString(), n.country.ToString());
                                res = true;
                                string[] results = new string[] { result.answer[0].rdata.ToString(), n.country.ToString() };
                                LogProp(new ListViewItem(results));                                
                            }
                            else
                            {
                                res = false;
                                string[] results = new string[] { "Failed", n.country.ToString() };
                                LogProp(new ListViewItem(results));
                            }
                        }
                    }
                }
                catch (WebException)
                {
                    //sometimes the open resolver fails
                    res = false;
                    string[] results = new string[] { "Server could not be reached", n.country.ToString() };
                    LogProp(new ListViewItem(results));
                }
            }

            SetStatusProp("Propagation Check Complete");

        }//execute operation to get the Propagation info
        //END PROP CODE---------------------------------------------------------

        //text report

        private void generateTextReport()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(System.Environment.CurrentDirectory + "/dnsreport.txt"))
            {
                file.WriteLine("--------------------");
                //A records
                file.WriteLine("A records for " + domain);
                if (strArecs != null) //make sure results were actually returned
                {
                    foreach (string s in strArecs) //iterate through the strings in the array
                    {
                        if (s != null) //if the string isn't empty, write it
                        {
                            file.WriteLine(s);
                        }
                    }
                }
                else
                {
                    file.WriteLine("Couldn't find any A records for " + domain);
                }
                //A records
                file.WriteLine("--------------------");
                //MX records
                file.WriteLine("MX records for " + domain);
                if (strMXrecs != null)
                {
                    foreach (string s in strMXrecs)
                    {
                        if (s != null)
                        {
                            file.WriteLine(s);
                        }
                    }
                }
                else
                {
                    file.WriteLine("Couldn't find any MX records for " + domain);
                }
                //end MX records
                file.WriteLine("--------------------");
                //NS records
                file.WriteLine("NS records for " + domain);
                if (strNSrecs != null)
                {
                    foreach (string s in strNSrecs) 
                    {
                        if (s != null) 
                        {
                            file.WriteLine(s);
                        }
                    }
                }
                else
                {
                    file.WriteLine("Couldn't find any NS records for " + domain);
                }
                //end NS records
                file.WriteLine("--------------------");
                //TXT records
                file.WriteLine("TXT records for " + domain);
                if (strTXTrecs != null)
                {
                    foreach (string s in strTXTrecs)
                    {
                        if (s != null)
                        {
                            file.WriteLine(s);
                        }
                    }
                }
                else
                {
                    file.WriteLine("Couldn't find any TXT records for " + domain);
                }
                //end TXT records
                file.WriteLine("--------------------");
                //authoritative nameservers
                
            }

            frmReport dnsReport = new frmReport();
            dnsReport.ShowDialog();
        }

        //end text report

        //form updaters --------------------------------------------------
        delegate void SetTextCallback(string text); //this delegate allows strings to be passed across threads 
                                                    //so that we can can take the output from the 
                                                    //background workers and update the form controls safely

        delegate void SetListViewCallBack(ListViewItem list);
        //updater for whois                                            
        private void LogWhois(string s)
        {
            if (this.lstWhois.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(LogWhois);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                lstWhois.Items.Add(s);
            }
        } //thread-safe write to lstWhois

        //updaters for DNS
        private void LogDNSDomain(string s)
        {
            if (this.lstDNS.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(LogDNSDomain);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                lstDNS.Items.Add(s);
            }
        } //thread-safe write to lstDNS

        private void LogDNSRecord(string s)
        {
            if (this.lstDNSRecord.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(LogDNSRecord);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                lstDNSRecord.Items.Add(s.ToUpper());
            }
        } //thread-safe write to lstDNSRecord

        private void LogDNSData(string s)
        {
            if (this.lstDNSData.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(LogDNSData);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                lstDNSData.Items.Add(s);
            }
        } //thread-safe write to lstDNSData

        private void LogDNSTTL(string s)
        {
            if (this.lstDNSTtl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(LogDNSTTL);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                lstDNSTtl.Items.Add(s);
            }
        } //thread-safe write to lstDNSTtl

        //updater for propagation
        
        private void LogProp(ListViewItem s)
        {
            if (this.lstPropagation.InvokeRequired)
            {
                SetListViewCallBack d = new SetListViewCallBack(LogProp);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                lstPropagation.Items.Add(s);
                if (res)
                {
                    int x = lstPropagation.Items.Count;
                    lstPropagation.Items[(x - 1)].BackColor = Color.LightGreen;
                    res = false;
                }
                else
                {
                    int x = lstPropagation.Items.Count;
                    lstPropagation.Items[(x - 1)].BackColor = Color.Salmon;
                    res = true;
                }
            }
        } //thread-safe write to lstPropagation
        
        //operation status updaters
        private void SetStatusWhois(string s)
        {
            if (this.lblStatusWhois.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetStatusWhois);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                lblStatusWhois.Text = s;
            }
        } //thread-safe write to lblStatusWhois

        private void SetStatusDNS(string s)
        {
            if (this.lblStatusDNS.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetStatusDNS);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                lblStatusDNS.Text = s;
            }
        } //thread-safe write to lblStatusWhois

        private void SetStatusProp(string s)
        {
            if (this.lblStatusProp.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetStatusProp);
                this.Invoke(d, new object[] { s });
            }
            else
            {
                lblStatusProp.Text = s;
            }
        } //thread-safe write to lblStatusWhois


        //miscellanious
        private void cmbPropType_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }//make sure no one can type a custom value in the textbox

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbPropType.Text = "A";

            ColumnHeader c = new ColumnHeader();
            c.Text = "Data";
            c.Name = "col1";
            c.Width = lstPropagation.Width - 125;
            lstPropagation.Scrollable = true;
            lstPropagation.HeaderStyle = ColumnHeaderStyle.None;
            lstPropagation.View = View.Details;
            lstPropagation.Columns.Add(c);

            ColumnHeader v = new ColumnHeader();
            v.Text = "Region";
            v.Name = "col2";
            v.Width = 121;
            v.TextAlign = HorizontalAlignment.Right;
            lstPropagation.Scrollable = true;
            lstPropagation.HeaderStyle = ColumnHeaderStyle.None;
            lstPropagation.View = View.Details;
            lstPropagation.Columns.Add(v);
        }//create the columns for the propagation on form load

        private void lstDNSData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(lstDNSData.SelectedItem.ToString());
        }//throw the selected text to the clipboard

        private void button1_Click(object sender, EventArgs e)
        {
            generateTextReport();
        }//generate the report

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            ListView.ListViewItemCollection items = lstPropagation.Items;
            ListViewItem[] items2 = new ListViewItem[24];
            int x = 0;
            foreach(ListViewItem l in items)
            {
                if (l != null)
                {
                    items2[x] = l;
                }
                x++;
            }

            lstPropagation.Clear();

            ColumnHeader c = new ColumnHeader();
            c.Text = "Data";
            c.Name = "col1";
            c.Width = lstPropagation.Width - 125;
            lstPropagation.Scrollable = true;
            lstPropagation.HeaderStyle = ColumnHeaderStyle.None;
            lstPropagation.View = View.Details;
            lstPropagation.Columns.Add(c);

            ColumnHeader v = new ColumnHeader();
            v.Text = "Region";
            v.Name = "col2";
            v.Width = 121;
            v.TextAlign = HorizontalAlignment.Right;
            lstPropagation.Scrollable = true;
            lstPropagation.HeaderStyle = ColumnHeaderStyle.None;
            lstPropagation.View = View.Details;
            lstPropagation.Columns.Add(v);

            foreach (ListViewItem l in items2)
            {
                if (l != null)
                {
                    lstPropagation.Items.Add(l);
                }
            }
        }//as the window size changes, change the size fo the columns in the propagation view

        private void lstWhois_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(lstWhois.SelectedItem.ToString());
        }//throw the selected text to the clipboard

        private void lstDNS_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = lstDNS.SelectedIndex;
            lstDNSData.SelectedIndex = x;
            lstDNSRecord.SelectedIndex = x;
            lstDNSTtl.SelectedIndex = x;
        }

        private void lstDNSRecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = lstDNSRecord.SelectedIndex;
            lstDNSData.SelectedIndex = x;
            lstDNS.SelectedIndex = x;
            lstDNSTtl.SelectedIndex = x;
        }

        private void lstDNSData_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = lstDNSData.SelectedIndex;
            lstDNS.SelectedIndex = x;
            lstDNSRecord.SelectedIndex = x;
            lstDNSTtl.SelectedIndex = x;
        }

        private void lstDNSTtl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = lstDNSTtl.SelectedIndex;
            lstDNSData.SelectedIndex = x;
            lstDNSRecord.SelectedIndex = x;
            lstDNS.SelectedIndex = x;
        }

        private void lstPropagation_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(lstPropagation.SelectedItems[0].SubItems[0].Text);
        }
    }

    public class Question
    {
        public string name { get; set; }
        public string type { get; set; }
        public string @class { get; set; }
    }

    public class Answer
    {
        public string name { get; set; }
        public string type { get; set; }
        public string @class { get; set; }
        public int ttl { get; set; }
        public int rdlength { get; set; }
        public string rdata { get; set; }
    }

    public class Node
    {
        public string name { get; set; }
        public string isocc { get; set; }
        public string country { get; set; }
        public string asn { get; set; }
        public string @operator { get; set; }
    }

    public class RootObject
    {
        public List<Question> question { get; set; }
        public List<Answer> answer { get; set; }
        public List<Node> nodes { get; set; }
    }
}
