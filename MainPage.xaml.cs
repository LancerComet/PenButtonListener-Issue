using System;
using System.Diagnostics;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Input;
using Windows.UI.Xaml;

namespace PenButtonListener {
  public sealed partial class MainPage : Page {
    private int _tailButtonPressedTimes;
    private void OnTailButtonClicked (Windows.Devices.Input.PenButtonListener sender, PenTailButtonClickedEventArgs args) {
      this.Results.Text = $"Tail Button Pressed {++this._tailButtonPressedTimes} times";
    }

    private void OnGCButtonClick (object sender, RoutedEventArgs e) {
      GC.Collect();
    }

    private void OnPenRegisterButtonClick (object sender, RoutedEventArgs e) {
      this.RegisterPen();
    }

    private void RegisterPen () {
      if (!ApiInformation.IsTypePresent("Windows.Devices.Input.PenButtonListener"))
        return;

      try {
        var penButtonListener = Windows.Devices.Input.PenButtonListener.GetDefault();
        Debug.WriteLine(penButtonListener.IsSupported());
        penButtonListener.TailButtonClicked += OnTailButtonClicked;
      } catch (Exception e) {
        Debug.WriteLine("Cannot support PenButtonListener due to {0}", e.Message);
      }
    }

    public MainPage () {
      this.InitializeComponent();
      this.RegisterPen();
    }
  }
}