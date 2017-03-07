﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M3Ueditor
{
    public partial class Main : Form
    {
        private SortableBindingList<TVChannel> channels = new SortableBindingList<TVChannel>(); // Список каналов
        private bool isChange { get; set; } = false;
        List<string> groupList { get; set; }    // Список групп

        public Main()
        {
            InitializeComponent();

            groupList = new List<string>();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e) => CheckChanged();


        #region Menu
        private void tsNew_Click(object sender, EventArgs e) => newListTV();
        private void tsOpen_Click(object sender, EventArgs e) => OpenListTV();
        private void tsSave_Click(object sender, EventArgs e) => SaveListTv();
        private void tsAdd_Click(object sender, EventArgs e) => Add();
        private void tsRemove_Click(object sender, EventArgs e) => Remove();
        private void tsUp_Click(object sender, EventArgs e) => Up();
        private void tsDown_Click(object sender, EventArgs e) => Down();

        #endregion


        #region Methods

        private void newListTV()
        {
            channels.Clear();

            string tvgName = "New Channel";
            string tvglogo = "New Logo";
            string groupTitle = "New Group";
            string Name = "New Channel";
            string udp = "udp://@224.1.1.1:6000";

            channels.Add(new TVChannel(
                        _tvgName: tvgName.Trim(),
                        _tvglogo: tvglogo.Trim(),
                        _groupTitle: groupTitle.Trim(),
                        _udp: udp.Trim(),
                        _Name: Name.Trim()
                        ));

            dgvTV.DataSource = channels;
        }

        private void OpenListTV()
        {
            CheckChanged();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Файлы плейлиста (*.m3u)|*.m3u|CSV files (*.csv)|*.csv";
            fileDialog.Title = "Открыть плейлист";
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = fileDialog.FileName;

                StreamReader playlist = new StreamReader(file);
                channels.Clear();
                //dgvTV.DataSource = channels;
                switch (Path.GetExtension(file))
                {
                    case ".m3u":
                        ParseM3U(playlist);
                        break;

                    case ".csv":
                        //ParseCSV();
                        break;
                }
                TableRefresh();
                UpdategroupList();
            }
        }

        private void SaveListTv()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Файлы плейлиста (*.m3u)|*.m3u|CSV files (*.csv)|*.csv";
            fileDialog.Title = "Сохранить плейлист";
            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter file = new StreamWriter(fileDialog.FileName, false, Encoding.UTF8);
                file.WriteLine("#EXTM3U");
                for (int i = 0; i < channels.Count; i++)
                {
                    file.WriteLine("#EXTINF:-1 "
                        + "tvg-name=\"" + channels[i].tvgName + "\" "
                        + "tvg-logo=\"" + channels[i].tvglogo + "\" "
                        + "group-title=\"" + channels[i].groupTitle + "\""
                        + "," + channels[i].tvgName);
                    file.WriteLine(channels[i].UDP);
                }
                file.Close();
            }
            isChange = false;
        }

        void UpdategroupList()
        {
            if (channels.Count > 0)
            {
                groupList.Clear();
                for (int i = 0; i < channels.Count; i++)
                {
                    groupList.Add(channels[i].groupTitle);
                }
                groupList = groupList.Distinct().ToList();
                groupTitleComboBox.Items.AddRange(groupList.ToArray());

                tree.Nodes.Clear();
                // tree.Nodes.Add("Все");

                TreeNode root = new TreeNode("Все");
                tree.Nodes.Add(root);
                //root.Nodes.Add(tn);

                if (!(groupList.Count == 1 && groupList[0] == ""))
                {
                    for (int i = 0; i < groupList.Count; i++)
                    {
                        root.Nodes.Add(groupList[i]);
                        // tree.Nodes.Add(groupList[i]);
                    }
                    tree.ExpandAll();
                }
            }
        }

        void Changed()
        {
            isChange = true;
            UpdategroupList();
        }

        private void CheckChanged()
        {
            if (isChange)
            {
                DialogResult dialog = MessageBox.Show("Файл изменен. Выполнить сохранение?!", "Предупреждение", MessageBoxButtons.OKCancel);
                if (dialog == DialogResult.OK)
                {
                    SaveListTv();
                }
            }
        }

        public void ParseM3U(StreamReader playlist)
        {
            string line = "";
            List<string> data = new List<string>();

            string tvgName = "N/A";
            string tvglogo = "N/A";
            string groupTitle = "N/A";
            string Name = "N/A";
            string udp = "N/A";

            while ((line = playlist.ReadLine()) != null)
            {
                if (line.StartsWith("#EXTINF"))
                {
                    tvgName = stringOperations.Between(line, "tvg-name=\"", "\"");
                    tvglogo = stringOperations.Between(line, "tvg-logo=\"", "\"");
                    groupTitle = stringOperations.Between(line, "group-title=\"", "\"");
                    Name = line.Split(',').Last();
                    continue;
                }
                else if (line.Contains("//"))
                {
                    udp = line;
                }
                else
                {
                    continue;
                }

                try
                {
                    channels.Add(new TVChannel(
                        _tvgName: tvgName.Trim(),
                        _tvglogo: tvglogo.Trim(),
                        _groupTitle: groupTitle.Trim(),
                        _udp: udp.Trim(),
                        _Name: Name.Trim()
                        ));
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("A channel has been omitted due to its incorrect format");
                    continue;
                }
            }
            playlist.Close();

            if (channels.Count == 0)
            {
                MessageBox.Show("Структура файла не распознана!", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                splitContainer1.Panel2Collapsed = false;
            }
        }

        private void Add()
        {
            string tvgName = "New Channel";
            string tvglogo = "New Logo";
            string groupTitle = "New Group";
            string Name = "New Channel";
            string udp = "udp://@224.1.1.1:6000";

            channels.Add(new TVChannel(
                        _tvgName: tvgName.Trim(),
                        _tvglogo: tvglogo.Trim(),
                        _groupTitle: groupTitle.Trim(),
                        _udp: udp.Trim(),
                        _Name: Name.Trim()
                        ));

            dgvTV.Rows[channels.Count - 1].Selected = true;
            dgvTV.FirstDisplayedScrollingRowIndex = channels.Count - 1;
            Changed();
        }

        private void Remove()
        {
            if (dgvTV.SelectedRows.Count == 0)
                return;

            int selectedRow = dgvTV.SelectedRows[0].Index;

            if (channels.Count > 1 && selectedRow - 1 > 0)
                dgvTV.Rows[selectedRow - 1].Selected = true;
            else if (channels.Count > 1 && selectedRow + 1 <= channels.Count - 1)
                dgvTV.Rows[selectedRow + 1].Selected = true;

            channels.RemoveAt(selectedRow);

            Changed();
        }

        private void Up()
        {
            if (dgvTV.SelectedRows.Count == 0)
                return;

            int selectedRow = dgvTV.SelectedRows[0].Index;
            TVChannel current = channels[selectedRow];
            TVChannel previous = channels[selectedRow - 1];
            channels.RemoveAt(selectedRow);

            channels.Insert(selectedRow -= 1, current);
            dgvTV.Rows[selectedRow].Selected = true;
        }

        private void Down()
        {
            if (dgvTV.SelectedRows.Count == 0)
                return;

            int selectedRow = dgvTV.SelectedRows[0].Index;
            TVChannel current = channels[selectedRow];
            TVChannel next = channels[selectedRow + 1];
            channels.RemoveAt(selectedRow);

            channels.Insert(selectedRow += 1, current);
            dgvTV.Rows[selectedRow].Selected = true;
        }

        #endregion



        #region Таблица
        void TableRefresh(string node = "", bool refresh = false)
        {
            TVChannel tvc = GetSelected();
            if (refresh)
            {
                //IEnumerable<TVChannel> sel = channels.Where(m => m.groupTitle == node);
                SortableBindingList<TVChannel> filteredList = new SortableBindingList<TVChannel>(channels.Where(m => m.groupTitle == node).ToList());
                dgvTV.DataSource = filteredList;
            }
            else
            {
                dgvTV.DataSource = channels;
            }
            if (tvc != null) Selected(dgvTV, tvc);
        }

        private void dgvTV_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTV.SelectedRows.Count == 0)
                return;

            int selectedRow = dgvTV.SelectedRows[0].Index;

            tvgNameBox.Text = channels[selectedRow].tvgName;
            tvglogoBox.Text = channels[selectedRow].tvglogo;
            groupTitleComboBox.Text = channels[selectedRow].groupTitle;
            UDPbox.Text = channels[selectedRow].UDP;
            NameBox.Text = channels[selectedRow].Name;
        }

        private void dgvTV_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // left click
            {
                if (Control.ModifierKeys == Keys.Control)
                {
                    System.Diagnostics.Debug.Print("CTRL + Left click!");
                    dgvTV.MultiSelect = true;
                }
                else
                {
                    System.Diagnostics.Debug.Print("Left click!");
                    dgvTV.MultiSelect = false;
                }
            }
        }

        private void dgvTV_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)   // Порядок 1
        {
            TableDragDrop(e);
        }
        private void TableDragDrop(DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView dgv = dgvTV;
                if (dgv != null && dgv.SelectedRows.Count > 0 && dgv.SelectedRows[0].Index > -1)
                    if (dgv.SelectedRows[0].Index == e.RowIndex) dgv.DoDragDrop(e.RowIndex, DragDropEffects.Copy);
            }
        }

        private void tree_DragDrop(object sender, DragEventArgs e)// здесь функционал DragDrop
        {
            Point pt = tree.PointToClient(new Point(e.X, e.Y));
            TreeNode destinationNode = tree.GetNodeAt(pt);
            TreeNode dragedNode = new TreeNode();

            TVChannel tvc = GetSelected();
            if (tvc != null)
            {
                try
                {
                    if (destinationNode != tree.TopNode)
                    {
                        tvc.groupTitle = destinationNode.Text;
                        TableRefresh();
                    }

                    //if (destinationNode == tree.TopNode)
                    //{
                    //    MessageBox.Show("Top");
                    //}
                    //else
                    //{
                    //    MessageBox.Show(destinationNode.Text);
                    //}


                    //if (destinationNode.Level == 0 && destinationNode.Index == 0)
                    //{ //если условие верно, то это главный узел
                    //  //MessageBox.Show(destinationNode.FullPath);
                    //}
                    //else
                    //{
                    //    MessageBox.Show(tvc.Name + " = " + destinationNode.Text);
                    //    string dirPath = Path.Combine(_videoCollection.Options.Source, destinationNode.FullPath);

                    //    if (File.Exists(Path.Combine(record.Path, record.FileName)))
                    //        if (Directory.Exists(dirPath))
                    //            File.Move(Path.Combine(record.Path, record.FileName), Path.Combine(dirPath, record.FileName));

                    //    record.DirName = destinationNode.Text;
                    //    record.Path = dirPath;

                    //    _videoCollection.Save();
                    //    PrepareRefresh();
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void tree_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private TVChannel GetSelected()
        {
            DataGridView dgv = dgvTV;
            if (dgv != null && dgv.SelectedRows.Count > 0 && dgv.SelectedRows[0].Index > -1)
            {
                TVChannel tvc = null;
                if (dgv.SelectedRows[0].DataBoundItem is TVChannel) tvc = dgv.SelectedRows[0].DataBoundItem as TVChannel;
                if (tvc != null) return tvc;

                //List<string> nnn = new List<string>();

                //foreach (DataGridViewTextBoxCell item in dgv.SelectedRows[0].Cells)
                //    if (item != null && item.Value != null)
                //        nnn.Add(item.Value.ToString());
            }
            return null;
        }

        private void Selected(DataGridView dgv, TVChannel tvc)
        {
            dgv.ClearSelection();
            foreach (DataGridViewRow row in dgv.Rows)
                if ((row.DataBoundItem as TVChannel).Name == tvc.Name)
                {
                    row.Selected = true;
                    dgv.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
        }
        #endregion


        #region Дерево
        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == tree.TopNode)
            {
                TableRefresh(e.Node.Text, false);
            }
            else
            {
                TableRefresh(e.Node.Text, true);
            }
        }

        #endregion


        #region Панель редактирования
        private void UserModifiedChanged(object sender, EventArgs e) => Modified(); // событие при измнении любого поля
        private void Modified()
        {
            dgvTV.DefaultCellStyle.SelectionBackColor = Color.Salmon; // подсветка редактируемой строки в таблице
            dgvTV.Enabled = false;   // блокировка таблицы
            tree.Enabled = false; // блокировка дерева
        }

        private void btnChangeApprove_Click(object sender, EventArgs e) // Кнопка Применить
        {
            if (dgvTV.SelectedRows.Count == 0)
                return;

            int selectedRow = dgvTV.SelectedRows[0].Index;

            channels[selectedRow].tvgName = tvgNameBox.Text;
            channels[selectedRow].tvglogo = tvglogoBox.Text;
            channels[selectedRow].groupTitle = groupTitleComboBox.Text;
            channels[selectedRow].UDP = UDPbox.Text;
            channels[selectedRow].Name = NameBox.Text;

            tree.Enabled = true;      // Разблокировка дерева
            dgvTV.Enabled = true;     // Разблокировка таблицы
            dgvTV.DefaultCellStyle.SelectionBackColor = Color.Silver;    // Восстановления цвета селектора таблицы

            TableRefresh();

            Changed();
        }

        private void btnChangeCancel_Click(object sender, EventArgs e)  // Кнопка Отмена
        {
            tree.Enabled = true;      // Разблокировка дерева
            dgvTV.Enabled = true;     // Разблокировка таблицы
            dgvTV.DefaultCellStyle.SelectionBackColor = Color.Silver;    // Восстановления цвета селектора таблицы
        }



        #endregion





    }
}
