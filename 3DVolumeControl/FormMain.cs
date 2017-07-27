using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Windows.Input;
using System.Diagnostics;

namespace _3DVolumeControl
{
  public partial class FormMain : Form
  {
    #region Constants and Declarations
    //
    // Constants and Declarations
    //
    private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
    private const int APPCOMMAND_VOLUME_UP = 0xA0000;
    private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
    private const int WM_APPCOMMAND = 0x319;

    [DllImport("user32.dll")]
    public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll")]
    static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    #endregion


    #region Variables
    //
    // Variables
    //    
    private _3DxMouse._3DxMouse m_3DxMouse;
    private DateTime m_lastUpdate = DateTime.Now;
    private NotifyIcon m_trayIcon = new NotifyIcon();
    private ContextMenu m_trayMenu = new ContextMenu();

    #endregion


    #region GUI-Events
    //
    // GUI Events
    //
    public FormMain()
    {
      InitializeComponent();

      // Init the 3D Mouse
      StartWndProcHandler();

      // Init the Trayicon
      m_trayMenu.MenuItems.Add("Exit", OnExit);
      m_trayIcon.Text = "3D Volume Control";
      m_trayIcon.Icon = Icon.FromHandle(_3DVolumeControl.Properties.Resources.Speaker.GetHicon());
      m_trayIcon.ContextMenu = m_trayMenu;
      m_trayIcon.Visible = true;

      var accessHandle = this.Handle;
    }

    private void FormMain_Load(object sender, EventArgs e)
    {
      // Make the form invisible
      Visible = false;
      this.Hide();
      ShowInTaskbar = false;
      base.OnLoad(e);
    }

    protected override void SetVisibleCore(bool value)
    {
      value = false;
      base.SetVisibleCore(value);
    }
        
    private void btnVolumeUp_Click(object sender, EventArgs e)
    {
      VolumeUp();
    }

    private void buttonMute_Click(object sender, EventArgs e)
    {
      SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_MUTE);
    }

    private void buttonVolumeDown_Click(object sender, EventArgs e)
    {
      VolumeDown();
    }
    
    #endregion


    #region Helper-Functions
    //
    // Helper Functions
    //
    private void OnExit(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void ChangeToMessageOnlyWindow()
    {
      IntPtr HWND_MESSAGE = new IntPtr(-3);
      SetParent(this.Handle, HWND_MESSAGE);
    }

    private void VolumeUp()
    {
      SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_UP);
    }

    private void VolumeDown()
    {
      SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_DOWN);
    }
    #endregion


    #region 3D-Mouse
    //
    // 3D Mouse
    //
    private void MotionEvent(object sender, _3DxMouse._3DxMouse.MotionEventArgs e)
    {      
      if (e.RotationVector != null)
      {
        DateTime currentTime = DateTime.Now;
        TimeSpan delta = currentTime - m_lastUpdate;

        // Let's use the z rotation to control the volume
        int volume = e.RotationVector.Z;
        Debug.WriteLine("Volume: " + volume.ToString());
        if (delta.Milliseconds > 350 - Math.Abs(volume))
        {          
          if (volume > 0)
          {
            VolumeUp();
          }
          else if (volume < 0)
          {
            VolumeDown();
          }
          m_lastUpdate = currentTime;
        }
      }
    }

    private void ButtonEvent(object sender, _3DxMouse._3DxMouse.ButtonEventArgs e)
    {
      Debug.WriteLine("Button pressed: " + e.ButtonMask.Pressed.ToString("X"));
    }

    protected override void WndProc(ref Message m)
    {
      if (m_3DxMouse != null)
      {
        // I could have done one of two things here.
        // 1. Use a Message as it was used before.
        // 2. Changes the ProcessMessage method to handle all of these parameters(more work).
        //    I opted for the easy way.

        // Note: Depending on your application you may or may not want to set the handled param.
        m_3DxMouse.ProcessMessage(m);
      }

      base.WndProc(ref m);
    }

    void StartWndProcHandler()
    {
      IntPtr hwnd = this.Handle;

      // Connect to Raw Input & find devices
      m_3DxMouse = new _3DxMouse._3DxMouse(hwnd);
      int NumberOf3DxMice = m_3DxMouse.EnumerateDevices();

      // Setup event handlers to be called when something happens
      m_3DxMouse.MotionEvent += new _3DxMouse._3DxMouse.MotionEventHandler(MotionEvent);
      m_3DxMouse.ButtonEvent += new _3DxMouse._3DxMouse.ButtonEventHandler(ButtonEvent);

      // Add devices to device list comboBox
      foreach (System.Collections.DictionaryEntry listEntry in m_3DxMouse.deviceList)
      {
        _3DxMouse._3DxMouse.DeviceInfo devInfo = (_3DxMouse._3DxMouse.DeviceInfo)listEntry.Value;
        Debug.WriteLine("Found device: " + devInfo.deviceName);
      }
    }
    #endregion
  }
}
