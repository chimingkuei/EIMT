using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMT
{
    interface IData
    {
        IEnumerable<(string, string[], string[])> Walk(string root_path);
        void CheckDir(string path);
        bool CheckFileFormat(string path);
    }

    class AOIHandler: IData
    {
        public IEnumerable<(string, string[], string[])> Walk(string root_path)
        {
            // 遍歷當前目錄下的所有文件和子目錄
            string[] files = Directory.GetFiles(root_path);
            string[] subDirs = Directory.GetDirectories(root_path);
            yield return (root_path, subDirs, files);

            // 遞歸遍歷子目錄
            foreach (string subDir in subDirs)
            {
                foreach (var tuple in Walk(subDir))
                {
                    yield return tuple;
                }
            }
        }

        public void CheckDir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void ConvertOR(Mat src, string output_file, double original_OR, double target_OR)
        {
            double magnification = original_OR / target_OR;
            Mat result_image = new Mat();
            Cv2.Resize(src, result_image, new OpenCvSharp.Size(Convert.ToInt32(src.Width * magnification), Convert.ToInt32(src.Height * magnification)));
            Cv2.ImWrite(output_file, result_image);
        }

        private Mat ResizeImage(Mat src, double magnification)
        {
            Mat result_image = new Mat();
            Cv2.Resize(src, result_image, new OpenCvSharp.Size(Convert.ToInt32(src.Width * magnification), Convert.ToInt32(src.Height * magnification)));
            return result_image;
        }

        public bool CheckFileFormat(string path)
        {
            bool state = true;
            string[] fileNames = Directory.GetFiles(path);
            foreach (string fileName in fileNames)
            {
                string fileExtension = Path.GetExtension(fileName).ToLower();
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                if (fileExtension != ".bmp" || !Int32.TryParse(fileNameWithoutExtension, out int result))
                {
                    state = false;
                }
            }
            return state;
        }

        public void Stitching(string input_dir, string output_dir, string direction, OpenCvSharp.ImreadModes color_type, double magnification)
        {
            int img_num = Directory.GetFiles(input_dir).Length;
            Mat result_image = new Mat();
            switch (direction)
            {
                case "Bottom to Top":
                    {
                        for (int i = 2; i <= img_num; i++)
                        {
                            if (i == 2)
                            {
                                Mat img1 = new Mat(Path.Combine(input_dir, "1.bmp"), color_type);
                                Mat img2 = new Mat(Path.Combine(input_dir, "2.bmp"), color_type);
                                Mat img1_resize = ResizeImage(img1, magnification);
                                Mat img2_resize = ResizeImage(img2, magnification);
                                Cv2.VConcat(new Mat[] { img2_resize, img1_resize }, result_image);
                            }
                            else
                            {
                                Mat img = new Mat(Path.Combine(input_dir, i.ToString() + ".bmp"), color_type);
                                Mat img_resize = ResizeImage(img, magnification);
                                Cv2.VConcat(new Mat[] { img_resize, result_image }, result_image);
                            }
                        }
                        Cv2.ImWrite(Path.Combine(output_dir, "VConcat.bmp"), result_image);
                        break;
                    }
                case "Top to Bottom":
                    {
                        for (int i = 2; i <= img_num; i++)
                        {
                            if (i == 2)
                            {
                                Mat img1 = new Mat(Path.Combine(input_dir, "1.bmp"), color_type);
                                Mat img2 = new Mat(Path.Combine(input_dir, "2.bmp"), color_type);
                                Mat img1_resize = ResizeImage(img1, magnification);
                                Mat img2_resize = ResizeImage(img2, magnification);
                                Cv2.VConcat(new Mat[] { img1_resize, img2_resize }, result_image);
                            }
                            else
                            {
                                Mat img = new Mat(Path.Combine(input_dir, i.ToString() + ".bmp"), color_type);
                                Mat img_resize = ResizeImage(img, magnification);
                                Cv2.VConcat(new Mat[] { result_image, img_resize }, result_image);
                            }
                        }
                        Cv2.ImWrite(Path.Combine(output_dir, "VConcat.bmp"), result_image);
                        break;
                    }
                case "Left to Right":
                    {
                        for (int i = 2; i <= img_num; i++)
                        {
                            if (i == 2)
                            {
                                Mat img1 = new Mat(Path.Combine(input_dir, "1.bmp"), color_type);
                                Mat img2 = new Mat(Path.Combine(input_dir, "2.bmp"), color_type);
                                Mat img1_resize = ResizeImage(img1, magnification);
                                Mat img2_resize = ResizeImage(img2, magnification);
                                Cv2.HConcat(new Mat[] { img1_resize, img2_resize }, result_image);
                            }
                            else
                            {
                                Mat img = new Mat(Path.Combine(input_dir, i.ToString() + ".bmp"), color_type);
                                Mat img_resize = ResizeImage(img, magnification);
                                Cv2.HConcat(new Mat[] { result_image, img_resize }, result_image);
                            }
                        }
                        Cv2.ImWrite(Path.Combine(output_dir, "HConcat.bmp"), result_image);
                        break;
                    }
                case "Right to Left":
                    {
                        for (int i = 2; i <= img_num; i++)
                        {
                            if (i == 2)
                            {
                                Mat img1 = new Mat(Path.Combine(input_dir, "1.bmp"), color_type);
                                Mat img2 = new Mat(Path.Combine(input_dir, "2.bmp"), color_type);
                                Mat img1_resize = ResizeImage(img1, magnification);
                                Mat img2_resize = ResizeImage(img2, magnification);
                                Cv2.HConcat(new Mat[] { img2_resize, img1_resize }, result_image);
                            }
                            else
                            {
                                Mat img = new Mat(Path.Combine(input_dir, i.ToString() + ".bmp"), color_type);
                                Mat img_resize = ResizeImage(img, magnification);
                                Cv2.HConcat(new Mat[] { img_resize, result_image }, result_image);
                            }
                        }
                        Cv2.ImWrite(Path.Combine(output_dir, "HConcat.bmp"), result_image);
                        break;
                    }
            }
        }

        public void MoveCutImage(Mat src, string output_dir, string file_name, Size size)
        {
            int col = (int)Math.Floor((double)(src.Width / size.Width));
            int row = (int)Math.Floor((double)(src.Height / size.Height));
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    Rect roi = new Rect(i * size.Width, j * size.Height, size.Width, size.Height);
                    Mat croppedImage = new Mat(src, roi);
                    Cv2.ImWrite(Path.Combine(output_dir, file_name + "_" + i.ToString() + "," + j.ToString() + ".bmp"), croppedImage);
                }
            }
        }


    }
}
