﻿using System;
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
        int gravity;
        int floorHeight;
        public ExtAttEdit()
        {
            InitializeComponent();
            attribFilePath = File.Open("..\\..\\..\\GDAPS2Game\\Content\\AttributeEditor\\attributes.dat", FileMode.OpenOrCreate);
            reader = new BinaryReader(attribFilePath);
            writer = new BinaryWriter(attribFilePath);
            
            
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            //convert values to ints
            gravity = (int)GravityUD.Value;
            floorHeight = (int)FloorUD.Value;

            //output to attributes.dat
            writer.Write(gravity);
            writer.Write(floorHeight);
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            //read in values
            gravity = reader.ReadInt32();
            floorHeight = reader.ReadInt32();

            //update values
            GravityUD.Value = gravity;
        }
    }
}
