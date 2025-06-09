using Newtonsoft.Json.Linq;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EIMT
{
    public class Parameter
    {
        #region Main Screen Parameter
        public string Source_Folder_val { get; set; }
        public string Output_Folder_val { get; set; }
        #endregion
        #region Conversion OR Parameter
        public string Source_OR_val { get; set; }
        public string Output_OR_val { get; set; }

        public bool OR_Color_val { get; set; }
        public bool OR_Gray_val { get; set; }

        public string Thread_Num_val { get; set; }
        #endregion
        #region Stitching Parameter
        public bool Stitching_Top_to_Bottom_val { get; set; }
        public bool Stitching_Bottom_to_Top_val { get; set; }
        public bool Stitching_Left_to_Right_val { get; set; }
        public bool Stitching_Right_to_Left_val { get; set; }
        public bool Stitching_Color_val { get; set; }
        public bool Stitching_Gray_val { get; set; }
        public string Magnification_val { get; set; }
        #endregion
        #region Moving Cutting Image Parameter
        public string Crop_Length_val { get; set; }
        public string Crop_Width_val { get; set; }
        #endregion
    }

    public partial class MainWindow : System.Windows.Window
    {
        // Evaluate Image Modification Tool
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Function
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("請問是否要關閉？", "確認", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void OpenDir(TextBox Control)
        {
            System.Windows.Forms.FolderBrowserDialog path = new System.Windows.Forms.FolderBrowserDialog();
            path.ShowDialog();
            Control.Text = path.SelectedPath;
        }

        private string AddRootPath(string originalpath, string targetpath)
        {
            string[] pathParts = Regex.Split(originalpath, @"\\|/");
            pathParts[0] = targetpath;
            string new_originalpath = string.Join("\\", pathParts);
            //Console.WriteLine(new_originalpath);
            return new_originalpath;
        }

        #region Config
        private void LoadConfig()
        {
            List<Parameter> Parameter_info = Config.Load();
            #region Main Screen Parameter
            Source_Folder.Text = Parameter_info[0].Source_Folder_val;
            Output_Folder.Text = Parameter_info[0].Output_Folder_val;
            #endregion
            #region Conversion OR Parameter
            Source_OR.Text = Parameter_info[0].Source_OR_val;
            Output_OR.Text = Parameter_info[0].Output_OR_val;
            OR_Color.IsChecked = Parameter_info[0].OR_Color_val;
            OR_Gray.IsChecked = Parameter_info[0].OR_Gray_val;
            Thread_Num.Text = Parameter_info[0].Thread_Num_val;
            #endregion
            #region Stitching Parameter
            Stitching_Top_to_Bottom.IsChecked = Parameter_info[0].Stitching_Top_to_Bottom_val;
            Stitching_Bottom_to_Top.IsChecked = Parameter_info[0].Stitching_Bottom_to_Top_val;
            Stitching_Left_to_Right.IsChecked = Parameter_info[0].Stitching_Left_to_Right_val;
            Stitching_Right_to_Left.IsChecked = Parameter_info[0].Stitching_Right_to_Left_val;
            Stitching_Color.IsChecked = Parameter_info[0].Stitching_Color_val;
            Stitching_Gray.IsChecked = Parameter_info[0].Stitching_Gray_val;
            Magnification.Text = Parameter_info[0].Magnification_val;
            #endregion
            #region Moving Cutting Image Parameter
            Crop_Length.Text = Parameter_info[0].Crop_Length_val;
            Crop_Width.Text = Parameter_info[0].Crop_Width_val;
            #endregion
        }

        private void SaveConfig()
        {
            List<Parameter> Parameter_config = new List<Parameter>()
                        {
                            new Parameter()
                            {
                                #region Main Screen Parameter
                                Source_Folder_val = Source_Folder.Text,
                                Output_Folder_val = Output_Folder.Text,
                                #endregion
                                #region Conversion OR Parameter
                                Source_OR_val = Source_OR.Text,
                                Output_OR_val = Output_OR.Text,
                                OR_Color_val=(bool)OR_Color.IsChecked,
                                OR_Gray_val=(bool)OR_Gray.IsChecked,
                                Thread_Num_val= Thread_Num.Text,
                                #endregion
                                #region Stitching Parameter
                                Stitching_Top_to_Bottom_val = (bool)Stitching_Top_to_Bottom.IsChecked,
                                Stitching_Bottom_to_Top_val = (bool)Stitching_Bottom_to_Top.IsChecked,
                                Stitching_Left_to_Right_val = (bool)Stitching_Left_to_Right.IsChecked,
                                Stitching_Right_to_Left_val = (bool)Stitching_Right_to_Left.IsChecked,
                                Stitching_Color_val = (bool)Stitching_Color.IsChecked,
                                Stitching_Gray_val = (bool)Stitching_Gray.IsChecked,
                                Magnification_val = Magnification.Text,
                                #endregion
                                #region Moving Cutting Image Parameter
                                Crop_Length_val = Crop_Length.Text,
                                Crop_Width_val = Crop_Width.Text,
                                #endregion
        }
    };
            Config.Save(Parameter_config);
            Logger.WriteLog("儲存參數!", 1, richTextBoxGeneral);
        }
        #endregion

        #region Dispatcher GetValue or SetValue
        public string TextBoxDispatcherGetValue(TextBox control)
        {
            string name = "";
            this.Dispatcher.Invoke(() =>
            {
                name = control.Text;
            });
            return name;

        }

        public double DoubleUpDownDispatcherGetValue(Xceed.Wpf.Toolkit.DoubleUpDown control)
        {
            double value = new double();
            this.Dispatcher.Invoke(() =>
            {
                value = Convert.ToDouble(control.Text);
            });
            return value;
        }

        public int IntegerUpDownDispatcherGetValue(Xceed.Wpf.Toolkit.IntegerUpDown control)
        {
            int value = new int();
            this.Dispatcher.Invoke(() =>
            {
                value = Convert.ToInt32(control.Text);
            });
            return value;
        }

        public bool RadioButtonDispatcherGetValue(RadioButton control)
        {
            bool value = new bool();
            this.Dispatcher.Invoke(() =>
            {
                value = (bool)control.IsChecked;
            });
            return value;

        }
        #endregion
        #endregion

        #region Parameter and Init
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadConfig();
        }
        BaseLogRecord Logger = new BaseLogRecord();
        BaseConfig<Parameter> Config = new BaseConfig<Parameter>();
        Core Do = new Core();
        #endregion

        #region Main Screen
        private void Main_Btn_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case nameof(Open_Source_Folder):
                    {
                        OpenDir(Source_Folder);
                        Logger.WriteLog("開啟影像資料夾路徑:" + Source_Folder.Text + "!", 1, richTextBoxGeneral);
                        break;
                    }
                case nameof(Open_Output_Folder):
                    {
                        OpenDir(Output_Folder);
                        Logger.WriteLog("開啟目的資料夾路徑:" + Output_Folder.Text + "!", 1, richTextBoxGeneral);
                        break;
                    }
                case nameof(OR_Scaling):
                    {
                        if (!string.IsNullOrEmpty(Source_Folder.Text))
                        {
                            if (!string.IsNullOrEmpty(Output_Folder.Text))
                            {
                                Task.Run(() =>
                                {
                                    var type = RadioButtonDispatcherGetValue(OR_Color) ? OpenCvSharp.ImreadModes.Color : OpenCvSharp.ImreadModes.Grayscale;
                                    foreach (var (dirPath, subDirs, files) in Do.Walk(TextBoxDispatcherGetValue(Source_Folder)))
                                    {
                                        Do.CheckDir(AddRootPath(dirPath, TextBoxDispatcherGetValue(Output_Folder)));
                                        ParallelOptions options = new ParallelOptions();
                                        options.MaxDegreeOfParallelism = IntegerUpDownDispatcherGetValue(Thread_Num);
                                        Parallel.ForEach(files, options, filePath =>
                                        {
                                            OpenCvSharp.Mat src = new OpenCvSharp.Mat(filePath, type);
                                            if (!src.Empty())
                                            {
                                                string newfilePath = AddRootPath(filePath, TextBoxDispatcherGetValue(Output_Folder));
                                                Do.ConvertOR(src, newfilePath, DoubleUpDownDispatcherGetValue(Source_OR), DoubleUpDownDispatcherGetValue(Output_OR));
                                            }
                                        }
                                        );
                                    }
                                    this.Dispatcher.Invoke(() =>
                                    {
                                        Logger.WriteLog("完成縮放光學解析度!", 1, richTextBoxGeneral);
                                    });

                                });
                            }
                            else
                            {
                                MessageBox.Show("請填入待處理影像資料夾路徑!", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("請填入輸出目的資料夾路徑!", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;
                    }
                case nameof(MoveCutImage):
                    {
                        if (!string.IsNullOrEmpty(Source_Folder.Text))
                        {
                            if (!string.IsNullOrEmpty(Output_Folder.Text))
                            {
                                string[] files = Directory.GetFiles(Source_Folder.Text);
                                OpenCvSharp.Size size = new OpenCvSharp.Size(Convert.ToInt32(Crop_Width.Text), Convert.ToInt32(Crop_Length.Text));
                                foreach (string filePath in files)
                                {
                                    Mat src = new Mat(filePath, ImreadModes.Color);
                                    string file_name = System.IO.Path.GetFileNameWithoutExtension(filePath);
                                    Do.MoveCutImage(src, Output_Folder.Text, file_name, size);
                                }
                                Logger.WriteLog("完成移動裁切影像!", 1, richTextBoxGeneral);
                            }
                            else
                            {
                                MessageBox.Show("請填入待處理影像資料夾路徑!", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("請填入輸出目的資料夾路徑!", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;
                    }
                case nameof(Stitching):
                    {
                        if (!string.IsNullOrEmpty(Source_Folder.Text))
                        {
                            if (!string.IsNullOrEmpty(Output_Folder.Text))
                            {
                                if (Do.CheckFileFormat(Source_Folder.Text))
                                {
                                    var color_type = (bool)Stitching_Color.IsChecked ? OpenCvSharp.ImreadModes.Color : OpenCvSharp.ImreadModes.Grayscale;
                                    string direction = null;
                                    if ((bool)Stitching_Top_to_Bottom.IsChecked)
                                        direction = "Top to Bottom";
                                    else if ((bool)Stitching_Bottom_to_Top.IsChecked)
                                        direction = "Bottom to Top";
                                    else if ((bool)Stitching_Left_to_Right.IsChecked)
                                        direction = "Left to Right";
                                    else if ((bool)Stitching_Right_to_Left.IsChecked)
                                        direction = "Right to Left";
                                    Do.Stitching(Source_Folder.Text, Output_Folder.Text, direction, color_type, Convert.ToDouble(Magnification.Text));
                                    Logger.WriteLog("完成拼接影像!", 1, richTextBoxGeneral);
                                }
                                else
                                {
                                    MessageBox.Show("請將檔案名稱改為整數、副檔名改為bmp類型!", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("請填入待處理影像資料夾路徑!", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("請填入輸出目的資料夾路徑!", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;
                    }
            }
        }
        #endregion

        #region Parameter Screen
        private void Parameter_Btn_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case nameof(Save_Config):
                    {
                        SaveConfig();
                        Logger.WriteLog("完成儲存參數!", 1, richTextBoxGeneral);
                        break;
                    }
            }
        }
        #endregion


    }
}
