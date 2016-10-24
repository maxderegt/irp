using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Project21
{
    public partial class clientGui : Form
    {
        public enum ConnectionState { Disconnected, Connected }
        private ConnectionState currentState = ConnectionState.Connected;

        Timer timer = new Timer();
        public Bike bike { get; set; }
        string comPort = string.Empty;

        public List<string> Messages { get; set; } = new List<string>();
        public List<string[]> Results { get; set; } = new List<string[]>();
        public List<string[]> graphdata { get; set; } = new List<string[]>();
        public bool doctor = false;
        private string[] data;
        private string clientname;
        private string selected;
        bool secondClient = false;
        private bool created = false;
        private bool graph1 = false;
        clientGui second;
        private Graph graph;

        private Timer download;

        private Client client;

        public static void Main(string[] args)
        {
            clientGui form = new clientGui();
            form.ShowDialog();
        }

        public clientGui()
        {
            InitializeComponent();

            download = new Timer();
            download.Tick += IncomingData;
            download.Interval = 1;
            
            //Create client, start function gets called when the user logs in
            client = new Client(Environment.UserName);
            
            textBox2.ReadOnly = true;
            textBox2.Multiline = true;
            textBox2.ScrollBars = ScrollBars.Both;
            textBox2.WordWrap = false;
            comboBox2.SelectedIndexChanged += combo2SelectedIndexChanged;
            comboBox3.SelectedIndexChanged += combo3SelectedIndexChanged;
            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            graph = new Graph();
            graph.Hide();
        }
        public clientGui(bool created)
        {
            InitializeComponent();
            this.created = created;
            client = new Client(Environment.UserName);
            Timer download = new Timer();
            download.Tick += IncomingData;
            download.Interval = 1;
            download.Start();
            textBox2.ReadOnly = true;
            textBox2.Multiline = true;
            textBox2.ScrollBars = ScrollBars.Both;
            textBox2.WordWrap = false;
            comboBox2.SelectedIndexChanged += combo2SelectedIndexChanged;
            comboBox3.SelectedIndexChanged += combo3SelectedIndexChanged;
            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            loginPanel.Hide();
            comPanel.Hide();
            clientPanel.Show();
            graph = new Graph();
            graph.Hide();
        }

        public Bike getBike()
        {
            return bike;
        }

        private void ClientData(Object sender, EventArgs e)
        {
            client.UploadQeue.Add("get_data_" + clientname);
        }

        private void IncomingData(Object sender, EventArgs e)
        {
            switch (currentState)
            {
                case ConnectionState.Connected:
                    handleIncomingData();
                    break;
                case ConnectionState.Disconnected:
                    //Stop timer that handles incoming data
                    download.Stop();
                    Utilities.showPopup("Connection lost", this, true);
                    break;
            }
        }

        private void handleIncomingData()
        {
            List<string> Downloaded = client.Downloaded;
            if (Downloaded.Count >= 1)
            {
                string command = Downloaded[0];
                char[] seperatingchar = new char[1];
                string cha = "_";
                seperatingchar[0] = System.Convert.ToChar(cha);
                string[] words = command.Split(seperatingchar);

                //handle your incoming shit
                if (words[0].Equals("account"))
                {
                    switch (words[1])
                    {
                        case "Login Succesfull":
                            loginPanel.Hide();
                            comPanel.Show();
                            comTextBox.Focus();
                            AcceptButton = comOKButton;
                            break;
                        case "Created":
                            loginPanel.Hide();
                            comPanel.Show();
                            comTextBox.Focus();
                            AcceptButton = comOKButton;
                            break;
                        case "Incorrect Credentials":
                            download.Stop();
                            client.Stop();
                            Utilities.showPopup("Incorrect Password", this, false);
                            infoLabel.Text = "Incorrect Password";
                            return;
                    }
                }
                else
                {
                    //Console.WriteLine("Received word: " + words[0]);
                    switch (words[0])
                    {
                        case "message":
                            string sent = words[1];
                            if (doctor)
                            {
                                if (comboBox3.SelectedItem != null)
                                    if (sent.Equals(comboBox3.SelectedItem.ToString()) || sent.Equals("Dokter"))
                                        textBox2.AppendText(Environment.NewLine + words[2]);
                            }
                            else
                            {
                                textBox2.AppendText(Environment.NewLine + words[2]);
                            }
                            break;
                        case "command":
                            bike.bikeCom.SendCommand(words[1]);
                            break;
                        case "graphdata":
                            if (!words[1].Equals("done"))
                            {
                                string[] separating = { " " };
                                string[] dat = words[1].Split(separating, System.StringSplitOptions.RemoveEmptyEntries);
                                graphdata.Add(dat);
                            }
                            else if (words[1].Equals("done")&&graph1 == false)
                            {
                                graph.graphdata = graphdata;
                                graph.updatechart();
                                graph.Show();
                            }
                            break;
                        case "meting":
                            string[] separatingChars = { " " };
                            data = words[1].Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                            Console.WriteLine(words[1]);
                            if (!Results.Contains(data))
                            {
                                //TODO: Remove
                                Console.WriteLine("Results added");
                                Results.Add(data);
                                UpdateGraph();
                            }
                            break;
                        case "doctor":
                            doctor = true;
                            if (!created)
                            {
                                second = new clientGui();
                            }
                            timer.Tick += Tick;
                            timer.Interval = 500;
                            timer.Start();
                            Timer getdata = new Timer();
                            getdata.Tick += ClientData;
                            getdata.Interval = 500;
                            getdata.Start();
                            client.UploadQeue.Add("get_connections");                            
                            clientPanel.Show();
                            chatBox.Focus();
                            comboBox1.Show();
                            button2.Show();
                            textBox3.Show();
                            comboBox3.Show();
                            button3.Show();
                            button4.Show();
                            client2Box.Show();
                            noodStop.Show();
                            textBox5.Show();
                            textBox1.Show();
                            textBox4.Show();
                            button5.Show();
                            button6.Show();
                            chatLabel.Text = "Chat met de cliënt";
                            comPanel.Hide();
                            loginPanel.Hide();
                            this.AutoSize = false;
                            this.Size = new Size(Size.Width, 576);
                            break;
                        case "client":
                            if (!comboBox3.Items.Contains(words[1]))
                                comboBox3.Items.Add(words[1]);
                            break;
                        case "No Connection":
                            currentState = ConnectionState.Disconnected;
                            return;
                    }
                }
                if(client.Downloaded.Count>0)
                client.Downloaded.Remove(Downloaded[0]);
            }
        }

        private void Gui_Load(object sender, EventArgs e)
        {
            comPanel.Parent = this;
            clientPanel.Parent = this;
            comPanel.Hide();
            comboBox1.Hide();
            button2.Hide();
            textBox3.Hide();
            comboBox3.Hide();
            button3.Hide();
            button4.Hide();
            client2Box.Hide();
            noodStop.Hide();
            textBox5.Hide();
            textBox1.Hide();
            textBox4.Hide();
            button5.Hide();
            button6.Hide();
        }

        private void comOKButton_Click(object sender, EventArgs e)
        {
            comPort = comTextBox.Text;
            comPanel.Hide();
            clientPanel.Show();
            AcceptButton = button1;
            if (!doctor)
            {
                bike = new Bike(comPort);
            }
            else
            {
                Timer data = new Timer();
                data.Tick += ClientData;
                data.Interval = 500;
                data.Start();
            }
            timer.Tick += Tick;
            timer.Interval = 500;
            timer.Start();
            if (!doctor)
            {
                client.UploadQeue.Add("get_messages_" + clientname);
            }
            if (doctor)
            {
                client.UploadQeue.Add("get_connections");
            }
        }

        private void logInButton_Click_1(object sender, EventArgs e)
        {
            if (accNameBox.Text == string.Empty || passBox.Text == string.Empty)
            {
                infoLabel.Text = "No name or password...";
                Utilities.showPopup("No name or password...", this, false);
            }
            else
            {
                //Start client and check if it was succesfull in connecting to the server
                if (!client.Start())
                {
                    //download.Stop();
                    Utilities.showPopup("Can't connect to server", this, false);
                    return;
                }
                //Start tcp incoming data and outgoing data
                download.Start();

                string name = accNameBox.Text;
                string pass = passBox.Text;
                if (name.Contains("dokter_"))
                {
                    client.UploadQeue.Add("account_" + name + "_password_" + pass + "_true");
                }
                else
                {
                    client.UploadQeue.Add("account_" + name + "_password_" + pass + "_false");
                }
                clientname = name;
                passBox.Clear();
            }
        }

        private void Tick(object sender, EventArgs e)
        {

            if (!doctor)
            {
                if (bike.bikeCom.BikeList.Count > 0)
                {
                    //updating the labels
                    client.UploadQeue.Add("bike_" + clientname + "_" +
                    bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].GetAll());
                    pulsev.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].pulse.ToString();
                    rpmv.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].rpm.ToString();
                    kmhv.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].kmh.ToString();
                    distancev.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].distance.ToString();
                    reqpowerv.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].reqPower.ToString();
                    energyv.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].energy.ToString();
                    minutesv.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].minutes.ToString();
                    secondsv.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].seconds.ToString();
                    actpowerv.Text = bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].actPower.ToString();

                    //updating the graph
                    switch (selected)
                    {
                        case "pulse":
                            chart1.Series[0].Points.Add(bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].pulse);
                            break;
                        case "rpm":
                            chart1.Series[0].Points.Add(bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].rpm);
                            break;
                        case "kmh":
                            chart1.Series[0].Points.Add(bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].kmh);
                            break;
                        case "distance":
                            chart1.Series[0].Points.Add(bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].distance);
                            break;
                        case "reqPower":
                            chart1.Series[0].Points.Add(bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].reqPower);
                            break;
                        case "energy":
                            chart1.Series[0].Points.Add(bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].energy);
                            break;
                        case "actPwer":
                            chart1.Series[0].Points.Add(bike.bikeCom.BikeList[bike.bikeCom.BikeList.Count - 1].actPower);
                            break;

                    }

                    if (chart1.Series[0].Points.Count == 30) chart1.Series[0].Points.RemoveAt(0);
                }
            }

        }

        private void UpdateGraph()
        {
            lock (Results)
            {
                if (Results.Count > 0)
                {
                    //updating the labels
                    pulsev.Text = Results[Results.Count - 1][3].ToString();
                    rpmv.Text = Results[Results.Count - 1][4].ToString();
                    kmhv.Text = Results[Results.Count - 1][5].ToString();
                    distancev.Text = Results[Results.Count - 1][6].ToString();
                    reqpowerv.Text = Results[Results.Count - 1][7].ToString();
                    energyv.Text = Results[Results.Count - 1][8].ToString();
                    minutesv.Text = Results[Results.Count - 1][9].ToString();
                    secondsv.Text = Results[Results.Count - 1][10].ToString();
                    actpowerv.Text = Results[Results.Count - 1][11].ToString();

                    //updating the graph
                    switch (selected)
                    {
                        case "pulse":
                            chart1.Series[0].Points.Add(Int32.Parse(Results[Results.Count - 1][3]));
                            break;
                        case "rpm":
                            chart1.Series[0].Points.Add(Int32.Parse(Results[Results.Count - 1][4]));
                            break;
                        case "kmh":
                            chart1.Series[0].Points.Add(Int32.Parse(Results[Results.Count - 1][5]));
                            break;
                        case "distance":
                            chart1.Series[0].Points.Add(Int32.Parse(Results[Results.Count - 1][6]));
                            break;
                        case "reqPower":
                            chart1.Series[0].Points.Add(Int32.Parse(Results[Results.Count - 1][7]));
                            break;
                        case "energy":
                            chart1.Series[0].Points.Add(Int32.Parse(Results[Results.Count - 1][8]));
                            break;
                        case "actPwer":
                            chart1.Series[0].Points.Add(Int32.Parse(Results[Results.Count - 1][11]));
                            break;

                    }
                }

                while (chart1.Series[0].Points.Count >= 30) chart1.Series[0].Points.RemoveAt(0);
            }
        }

        private void combo2SelectedIndexChanged(object sender, System.EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            selected = comboBox2.SelectedItem.ToString();
        }
        private void combo3SelectedIndexChanged(object sender, System.EventArgs e)
        {
            clientname = comboBox3.SelectedItem.ToString();
            chart1.Series[0].Points.Clear();
            textBox2.Clear();
            client.UploadQeue.Add("get_alldata_" + comboBox3.SelectedItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (doctor)
            {
                string message = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss ") + " Dokter : " + chatBox.Text;
                client.UploadQeue.Add("message_" + clientname + "_" + message);
            }
            else
            {
                string message = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss ") + clientname + " : " + chatBox.Text;
                client.UploadQeue.Add("message_" + clientname + "_" + message);
            }
            chatBox.Clear();
            client.UploadQeue.Add("update_" + clientname);

        }

        private void Gui_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox3.SelectedItem != null)
            if (!comboBox1.SelectedItem.ToString().Equals("RS"))
            {
                client.UploadQeue.Add("command_" + comboBox3.SelectedItem.ToString() + "_" +
                                      comboBox1.SelectedItem.ToString() + " " + textBox3.Text);
            }
            else
            {
                client.UploadQeue.Add("command_" + comboBox3.SelectedItem.ToString() + "_" +
                                         comboBox1.SelectedItem.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            client.UploadQeue.Add("get_connections");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string message = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss ") + " Dokter Broadcast : " + chatBox.Text;
            client.UploadQeue.Add("message_all_" + message);
            chatBox.Clear();
        }

        private void loginPanel_Enter(object sender, EventArgs e)
        {
            AcceptButton = logInButton;
            accNameBox.Focus();
        }

        private void chatBox_Enter(object sender, EventArgs e)
        {
            chatBox.Clear();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!secondClient)
            {
                secondClient = true;
                second.Show();
                second.Location = new Point(this.Location.X, this.Location.Y + 576);
            }
            else
            {
                secondClient = false;
                second.Hide();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int result;
            if(comboBox3.SelectedItem != null)
            if (Int32.TryParse(textBox5.Text, out result) && Int32.TryParse(textBox1.Text, out result) &&
                Int32.TryParse(textBox4.Text, out result))
            {
                if (Int32.Parse(textBox5.Text) > 99) textBox5.Text = "99";
                if (Int32.Parse(textBox1.Text) > 59) textBox5.Text = "59";
                if (Int32.Parse(textBox5.Text) > 999) textBox5.Text = "999";
                if (Int32.Parse(textBox5.Text) < 0) textBox5.Text = "0";
                if (Int32.Parse(textBox1.Text) < 0) textBox5.Text = "0";
                if (Int32.Parse(textBox4.Text) < 0) textBox5.Text = "0";
                client.UploadQeue.Add("command_"+ comboBox3.SelectedItem.ToString() + "_PT " + textBox5.Text + textBox1.Text);
                client.UploadQeue.Add("command_" + comboBox3.SelectedItem.ToString() + "_PD " + textBox4.Text);
            }
        }

        private void noodStop_Click(object sender, EventArgs e)
        {
            client.UploadQeue.Add("emergency_" + clientname);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                client.UploadQeue.Add("get_graphdata_" + comboBox3.SelectedItem.ToString());
            }
        }
    }
}
