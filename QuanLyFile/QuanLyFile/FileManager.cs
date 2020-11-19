using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyFile
{
    public partial class fileManager : Form
    {
        public fileManager()
        {
            InitializeComponent();
        }

        private void fileManager_Load(object sender, EventArgs e)
        {
            TreeNode tNode;
            tNode = treeFolder.Nodes.Add("2019");
            tNode = treeFolder.Nodes.Add("2020");

            treeFolder.Nodes[1].Nodes.Add("Cadisun-31/12/2020");
            Common.GetDBConnection();
        }
    }
}
