using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Windows;



namespace littleBeds
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            renderWindowData();


        }
  
        public List<String> readFromXML(String xmlFilename, String xmlDescendant)
        {
            List<string> tempList = new List<string>();
            XDocument xmlDoc = XDocument.Load(xmlFilename);
            var currentDesc = xmlDoc.Descendants(xmlDescendant);
            foreach (var xmlComponent in currentDesc)
            {
                string xmlToString = xmlComponent.Value.ToString();
                tempList.Add(xmlToString);
            }
            return tempList;
        }

        public List<String> daysUntilFromList(List<string> listname)
        {
            List<string> tempList = new List<string>();
            DateTime thisDay = DateTime.Today;
            foreach (String stringDate in listname)
            {
                DateTime currentDate = DateTime.Parse(stringDate);
                string daysUntil = (currentDate - thisDay).Days.ToString();
                tempList.Add(daysUntil);
            }
            return tempList;

        }


        public string[,] buildAndReturnArray()
        {
            var nameOfEventList = readFromXML("config.xml", "NameOf");
            var dateOfEventList = readFromXML("config.xml", "EventDate");
            var daysUntilList = daysUntilFromList(dateOfEventList);
            int arrayLength = nameOfEventList.Count;
            string[,] convertedLists = new string[arrayLength, 3];
            for (int i = 0; i < arrayLength; i++)
            {
                convertedLists[i,0] = nameOfEventList[i];
            }
            for (int i = 0; i < arrayLength; i++)
            {
                convertedLists[i,1] = dateOfEventList[i];
            }
            for (int i = 0; i < arrayLength; i++)
            {
                convertedLists[i,2] = daysUntilList[i];
            }

            return convertedLists;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            renderWindowData();
        }
        private void renderWindowData()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Event", "Event");
            dataGridView1.Columns.Add("Date Of Event", "Date Of Event");
            dataGridView1.Columns.Add("Days Until Event", "Days Until Event");
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
         //   dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToOrderColumns = false;
            var data = buildAndReturnArray();
            var rowCount = data.GetLength(0);
            var rowLength = data.GetLength(1);
            for (int rowIndex = 0; rowIndex < rowCount; ++rowIndex)
            {
                var row = new DataGridViewRow();

                for (int columnIndex = 0; columnIndex < rowLength; ++columnIndex)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell()
                    {
                        Value = data[rowIndex, columnIndex]
                    });
                }

                dataGridView1.Rows.Add(row);
            }
        }
    }
}
