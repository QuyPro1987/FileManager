using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyFile
{
    public partial class fileManager : Form
    {
        private string folderPath = "C:\\TEMP";
        public fileManager()
        {
            InitializeComponent();
        }

        private void fileManager_Load(object sender, EventArgs e)
        {
            treeView_load(sender, e);
            Common.GetDBConnection();
        }

        //Tạo cây thư mục
        private void treeView_load(object sender, EventArgs e)
        {
            //Liệt kê danh sách ổ đĩa
            string[] drives = Directory.GetDirectories(folderPath);
            foreach (string drive in drives)
            {
                DriveInfo di = new DriveInfo(drive);

                TreeNode node = new TreeNode(drive);
                node.Tag = drive;

                if (di.IsReady == true)
                    node.Nodes.Add("...");

                treeFolder.Nodes.Add(node);
            }
        }

        //sự kiện ẩn hiện folder con trong folder cha
        private void treeFolder_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.Nodes[0].Text == "..." && e.Node.Nodes[0].Tag == null)
                {
                    e.Node.Nodes.Clear();

                    //Liệt kê danh sách thư mục con
                    string[] dirs = Directory.GetDirectories(e.Node.Tag.ToString());

                    foreach (string dir in dirs)
                    {
                        DirectoryInfo di = new DirectoryInfo(dir);
                        TreeNode node = new TreeNode(di.Name, 0, 1);

                        try
                        {
                            //Lưu đường dẫn vào thuộc tính tag của node để sau này sử dụng
                            node.Tag = dir;

                            //Kiểm tra xem có thư mục con hay không thì thêm 3 dấu chấm
                            if (di.GetDirectories().Count() > 0)
                                node.Nodes.Add(null, "...", 0, 0);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            //Hiển thị Icon thư mục đã bị khóa
                            node.ImageIndex = 1;
                            node.SelectedImageIndex = 1;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "DirectoryLister",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            e.Node.Nodes.Add(node);
                        }
                    }
                }
            }
        }
    }
}
