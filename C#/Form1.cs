using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace LEDWizDemo
{
    public partial class Form1 : Form
    {
		private LEDWiz m_ledWiz = null;

		private Size m_ledButtonSize = new Size(8, 4);
		private CheckBox[] m_checkBoxArray = null;

		private Random m_random = null;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			m_checkBoxArray = new CheckBox[LEDWiz.MAX_LEDCOUNT];
			m_random = new Random();

			CreateLEDButtons();

			m_ledWiz = new LEDWiz(this);
			m_ledWiz.OnUsbDeviceAttached += new LEDWiz.UsbDeviceAttachedDelegate(OnUsbDeviceAttached);
			m_ledWiz.OnUsbDeviceRemoved += new LEDWiz.UsbDeviceRemovedDelegate(OnUsbDeviceRemoved);
			m_ledWiz.Initialize();

			m_ledWiz.SetLEDIntensityAll(LEDWiz.MAX_INTENSITY);
			m_ledWiz.SetLEDStateAll(true);

			UpdateDeviceList();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			m_ledWiz.SetLEDIntensityAll(0);
			m_ledWiz.SetLEDStateAll(false);

			m_ledWiz.Shutdown();
		}

		private void OnUsbDeviceAttached(int id)
		{
			UpdateDeviceList();
		}

		private void OnUsbDeviceRemoved(int id)
		{
			UpdateDeviceList();
		}

		private void CreateLEDButtons()
		{
			for (int y = 0; y < m_ledButtonSize.Height; y++)
			{
				for (int x = 0; x < m_ledButtonSize.Width; x++)
				{
					int id = y * m_ledButtonSize.Width + x;
					Size size = new Size(panel1.Width / m_ledButtonSize.Width, panel1.Height / m_ledButtonSize.Height);

					m_checkBoxArray[id] = new CheckBox();
					m_checkBoxArray[id].Location = new Point(x * size.Width, y * size.Height);
					m_checkBoxArray[id].Size = size;
					m_checkBoxArray[id].Appearance = Appearance.Button;
					m_checkBoxArray[id].Text = id.ToString();
					m_checkBoxArray[id].TextAlign = ContentAlignment.MiddleCenter;
					m_checkBoxArray[id].Checked = true;
					m_checkBoxArray[id].Click += new EventHandler(LEDButton_Click);

					panel1.Controls.Add(m_checkBoxArray[id]);
				}
			}
		}

		private void UpdateDeviceList()
		{
			lvwDevices.Items.Clear();

			for (int i = 0; i < m_ledWiz.DeviceCount; i++)
				lvwDevices.Items.Add(new ListViewItem(new string[] { m_ledWiz.GetDeviceType(i).ToString(), m_ledWiz.GetVendorId(i).ToString(), m_ledWiz.GetProductId(i).ToString(), m_ledWiz.GetVersionNumber(i).ToString(), m_ledWiz.GetVendorName(i), m_ledWiz.GetProductName(i), m_ledWiz.GetSerialNumber(i), m_ledWiz.GetDevicePath(i) }));

			for (int i = 0; i < lvwDevices.Columns.Count; i++)
				lvwDevices.Columns[i].Width = -2;

			if (lvwDevices.Items.Count > 0)
			{
				lvwDevices.SelectedIndices.Clear();
				lvwDevices.SelectedIndices.Add(0);
			}
		}

		private void LEDButton_Click(object sender, EventArgs e)
		{
			if (lvwDevices.SelectedIndices.Count == 0)
				return;

			int deviceId = lvwDevices.SelectedIndices[0];

			CheckBox checkBox = (CheckBox)sender;
			int ledIndex = Int32.Parse(checkBox.Text);

			m_ledWiz.LEDState[deviceId][ledIndex] = checkBox.Checked;

			m_ledWiz.SetLEDState(deviceId);
		}

		private void butAllLEDsOn_Click(object sender, EventArgs e)
		{
			if (lvwDevices.SelectedIndices.Count == 0)
				return;

			int deviceId = lvwDevices.SelectedIndices[0];

			SetLEDStateAll(deviceId, true);
		}

		private void butAllLEDsOff_Click(object sender, EventArgs e)
		{
			if (lvwDevices.SelectedIndices.Count == 0)
				return;

			int deviceId = lvwDevices.SelectedIndices[0];

			SetLEDStateAll(deviceId, false);
		}

		private void butAllLEDsRandom_Click(object sender, EventArgs e)
		{
			if (lvwDevices.SelectedIndices.Count == 0)
				return;

			int deviceId = lvwDevices.SelectedIndices[0];

			for (int i = 0; i < LEDWiz.MAX_LEDCOUNT; i++)
			{
				bool value = (m_random.Next(2) == 1 ? true : false);
				m_checkBoxArray[i].Checked = value;
				m_ledWiz.LEDState[deviceId][i] = value;
			}

			m_ledWiz.SetLEDState(deviceId);
		}

		private void trkIntensity_Scroll(object sender, EventArgs e)
		{
			if (lvwDevices.SelectedIndices.Count == 0)
				return;

			int deviceId = lvwDevices.SelectedIndices[0];

			if ((int)nudLEDNumber.Value == 0)
			{
				for (int i = 0; i < LEDWiz.MAX_LEDCOUNT; i++)
					m_ledWiz.LEDIntensity[deviceId][i] = (byte)trkIntensity.Value;
			}
			else
				m_ledWiz.LEDIntensity[deviceId][(int)nudLEDNumber.Value - 1] = (byte)trkIntensity.Value;

			m_ledWiz.SetLEDIntensity(deviceId);
		}

		private void nudPulseSpeed_ValueChanged(object sender, EventArgs e)
		{
			m_ledWiz.PulseSpeed = (int)nudPulseSpeed.Value;
		}

		private void butPulse_Click(object sender, EventArgs e)
		{
			if (lvwDevices.SelectedIndices.Count == 0)
				return;

			int deviceId = lvwDevices.SelectedIndices[0];
			LEDWiz.AutoPulseMode autoPulseMode = LEDWiz.AutoPulseMode.RampUp_RampDown;

			if (rdoRampUpRampDown.Checked)
				autoPulseMode = LEDWiz.AutoPulseMode.RampUp_RampDown;
			else if (rdoOnOff.Checked)
				autoPulseMode = LEDWiz.AutoPulseMode.On_Off;
			else if (rdoOnRampDown.Checked)
				autoPulseMode = LEDWiz.AutoPulseMode.On_RampDown;
			else if (rdoRampUpOn.Checked)
				autoPulseMode = LEDWiz.AutoPulseMode.RampUp_On;

			if ((int)nudLEDNumber.Value == 0)
			{
				for (int i = 0; i < LEDWiz.MAX_LEDCOUNT; i++)
					m_ledWiz.LEDIntensity[deviceId][i] = (byte)autoPulseMode;
			}
			else
				m_ledWiz.LEDIntensity[deviceId][(int)nudLEDNumber.Value - 1] = (byte)autoPulseMode;

			m_ledWiz.SetLEDIntensity(deviceId);
		}

		private void SetLEDStateAll(int deviceId, bool value)
		{
			for (int i = 0; i < LEDWiz.MAX_LEDCOUNT; i++)
			{
				m_checkBoxArray[i].Checked = value;
				m_ledWiz.LEDState[deviceId][i] = value;
			}

			m_ledWiz.SetLEDState(deviceId);
		}
    }
}