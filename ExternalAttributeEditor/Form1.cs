using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace ExternalAttributeEditor
{
    public partial class ExtAttEdit : Form
    {
        BinaryReader reader;
        BinaryWriter writer;
        Stream attribFilePath;
        int screenWidth;
        int screenHeight;
        int gravity;
        int floorHeight;
        int jumpHeight;
        public ExtAttEdit()
        {
            InitializeComponent();
            attribFilePath = File.Open("..\\..\\..\\GDAPS2Game\\Content\\attributes.dat", FileMode.OpenOrCreate);
            DefaultButton.Enabled = false;
            
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                attribFilePath = File.Open("..\\..\\..\\GDAPS2Game\\Content\\attributes.dat", FileMode.OpenOrCreate);

            }catch { }
            //convert values to ints
            writer = new BinaryWriter(attribFilePath);

            screenWidth = (int)widthUD.Value;
            screenHeight = (int)heightUD.Value;
            gravity = (int)GravityUD.Value;
            floorHeight = (int)FloorUD.Value;
            jumpHeight = (int)jumpUD.Value;
            //output to attributes.dat
            writer.Write(screenWidth);
            writer.Write(screenHeight);
            writer.Write(gravity);
            writer.Write(floorHeight);
            writer.Write(jumpHeight);

            writer.Flush();
            writer.Close();
            SaveButton.Enabled = false;
            DefaultButton.Enabled = true;
            writer = null;

        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            try
            {
                attribFilePath = File.Open("..\\..\\..\\GDAPS2Game\\Content\\attributes.dat", FileMode.OpenOrCreate);

            }
            catch { }
            reader = new BinaryReader(attribFilePath);
            //read in values
            screenWidth = reader.ReadInt32();
            screenHeight =reader.ReadInt32();

            gravity = reader.ReadInt32();
            floorHeight = reader.ReadInt32();
            jumpHeight = reader.ReadInt32();

            //update values
            GravityUD.Value = gravity;
            reader.Close();
            reader = null;
        }

        private void DefaultButton_Click(object sender, EventArgs e)
        {
            widthUD.Value = 1600;
            heightUD.Value = 900;
            GravityUD.Value = 1;
            FloorUD.Value = 750;
            jumpUD.Value = -25;
            attribFilePath = File.Open("..\\..\\..\\GDAPS2Game\\Content\\attributes.dat", FileMode.OpenOrCreate);
            writer = new BinaryWriter(attribFilePath);

            DefaultButton.Enabled = false;
            SaveButton.Enabled = true;
        }
    }
}
