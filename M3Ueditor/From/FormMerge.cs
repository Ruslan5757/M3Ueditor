﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace M3Ueditor
{
    public partial class FormMerge : Form
    {
        public SortableBindingList<TVChannelMerge> NewChannels { get; set; } // Новый список каналов
        public SortableBindingList<TVChannel> ModChannels { get; set; } // Новый список каналов

        public FormMerge()
        {
            InitializeComponent();
        }

        public FormMerge(SortableBindingList<TVChannel> tvcCurrent, SortableBindingList<TVChannel> tvcMerge)
        {
            InitializeComponent();
            this.Icon = M3Ueditor.Properties.Resources.m3u_icon;

            NewChannels = new SortableBindingList<TVChannelMerge>();
            ModChannels = new SortableBindingList<TVChannel>();

            dgvMerge.AutoGenerateColumns = false;

            foreach (var item in tvcCurrent)
            {
                NewChannels.Add(new TVChannelMerge(
                                _tvgName: item.TvgName,
                                _tvglogo: item.Tvglogo,
                                _groupTitle: item.GroupTitle,
                                _udp: item.UDP,
                                _Name: item.Name,
                                _check: true));
            }

            foreach (var item in tvcMerge)
            {
                NewChannels.Add(new TVChannelMerge(
                _tvgName: item.TvgName,
                _tvglogo: item.Tvglogo,
                _groupTitle: item.GroupTitle,
                _udp: item.UDP,
                _Name: item.Name,
                _check: false));
            }
            dgvMerge.DataSource = NewChannels;
        }


        private void button6_Click(object sender, EventArgs e)
        {

            dgvMerge.ColumnCount = 3;
            dgvMerge.Columns[0].Name = "Product ID";
            dgvMerge.Columns[1].Name = "Product Name";
            dgvMerge.Columns[2].Name = "Product Price";

            string[] row = new string[] { "1", "Product 1", "1000" };
            dgvMerge.Rows.Add(row);
            row = new string[] { "2", "Product 2", "2000" };
            dgvMerge.Rows.Add(row);
            row = new string[] { "3", "Product 3", "3000" };
            dgvMerge.Rows.Add(row);
            row = new string[] { "4", "Product 4", "4000" };
            dgvMerge.Rows.Add(row);

            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            dgvMerge.Columns.Add(chk);
            chk.HeaderText = "Check Data";
            chk.Name = "chk";
            dgvMerge.Rows[2].Cells[3].Value = true;
        }

        private void cBoxSelectDubl_CheckedChanged(object sender, EventArgs e)
        {
            int count = 0;
            // dataGridView1.Sort(dataGridView1.collu, direction);
            dgvMerge.Sort(dgvMerge.Columns[3], ListSortDirection.Ascending);

            for (int ThisRow = 0; ThisRow < dgvMerge.Rows.Count - 1; ThisRow++)
            {
                DataGridViewRow CompareRow = dgvMerge.Rows[ThisRow];
                for (int NextRow = ThisRow + 1; NextRow < dgvMerge.Rows.Count; NextRow++)
                {
                    DataGridViewRow row = dgvMerge.Rows[NextRow];
                    bool DuplicateRow = true;

                    string aa = CompareRow.Cells[1].Value.ToString();

                    if ((CompareRow.Cells[3].Value.ToString()) == (row.Cells[3].Value.ToString()))
                    {
                        //dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Red;
                        //row.DefaultCellStyle.ForeColor = Color.Silver;
                        //row.DefaultCellStyle.Font = new Font(TableRec.Font, FontStyle.Strikeout);
                        // dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red; 

                        row.DefaultCellStyle.ForeColor = Color.Silver;
                        row.DefaultCellStyle.Font = new Font(dgvMerge.Font, FontStyle.Strikeout);
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                        row.Cells[5].Value = true;
                        count++;

                        //row.BackColor = System.Drawing.Color.Red;
                        //CompareRow.BackColor = System.Drawing.Color.Red;

                        CompareRow.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                    }
                    else if (DuplicateRow)
                    {
                        DuplicateRow = false;
                    }
                }
            }
            infolabel.Text = "Количество дубликатов: " + count;
        }




        private void dgvMerge_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow _row = dgvMerge.Rows[e.RowIndex];
            if (_row.DefaultCellStyle.BackColor == Color.Silver)
            {
                _row.DefaultCellStyle.BackColor = Color.LightYellow;
            }
            else
            {
                _row.DefaultCellStyle.BackColor = Color.Silver;
            }

            return;

            // DataGridViewRow _row = dgvMerge.Rows[e.RowIndex];



            string message = string.Empty;
            foreach (DataGridViewRow row in dgvMerge.Rows)
            {
                //bool isSelected = Convert.ToBoolean(row.Cells["checkBoxColumn"].Value);
                bool isSelected = Convert.ToBoolean(row.Cells[5].Value);
                if (isSelected)
                {
                    if (row.Cells[4] != null)
                    {
                        //message += Environment.NewLine;
                        message += row.Cells[4].Value.ToString();
                    }

                }
            }

            MessageBox.Show("Selected Values" + message);

            return;

            if (e.ColumnIndex == 5)
            {
                DataGridView dgv = (DataGridView)sender;
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dgv.Rows[e.RowIndex].Cells[5];
                if (chk.Value == chk.FalseValue || chk.Value == null)
                {
                    chk.Value = chk.TrueValue;
                }
                else
                {
                    chk.Value = chk.FalseValue;
                }

                //if (chk.Value == chk.FalseValue || chk.Value == null)
                //{
                //    chk.Value = chk.TrueValue;
                //}
                //else
                //{
                //    chk.Value = chk.FalseValue;
                //}
            }


            //if (row.Cells[5].Value == true)
            //{

            //}
        }

        private void dgvMerge_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex > -1)
            {
                if (dgvMerge.Rows[e.RowIndex].Cells[5].Value.ToString() == "true")
                {
                    MessageBox.Show("true");
                }
                else
                {
                    MessageBox.Show("Test");
                }
                //if (dgvMerge.Rows[e.RowIndex].Cells[5].Value.ToString() == "true" || dgvMerge.Rows[e.RowIndex].Cells[5].Value.ToString() == "True")
                //    dgvMerge[1, e.RowIndex].Selected = true;
                //else
                //    dgvMerge[1, e.RowIndex].Selected = false;
            }
        }

        private void dgvMerge_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                Debug.WriteLine(" - CellValueChanged: " + e.ColumnIndex.ToString() + dgvMerge.Rows[e.RowIndex].Cells[5].Value);
            }
            else
            {
                Debug.WriteLine(" - CellValueChanged: -1 ");
            }

            //if (dgvMerge.Rows[e.RowIndex].Cells[5].Value.ToString() == "true")
            //{

            //}

            if (e.RowIndex > -1)
                if (Convert.ToBoolean(dgvMerge.Rows[e.RowIndex].Cells[5].Value))
                {
                    dgvMerge.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dgvMerge.Font, FontStyle.Strikeout);
                    dgvMerge.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    dgvMerge.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(dgvMerge.Font, FontStyle.Underline);
                    dgvMerge.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.SteelBlue;
                }


            //if (Convert.ToBoolean(row.Cells[1].Value))
            //{
            //    // what you want to do
            //}
        }

        private void dgvMerge_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            object obj = null;
            if (e.RowIndex > -1)
            {
                obj = dgvMerge.Rows[e.RowIndex].Cells[5].Value;
            }

            if (obj != null)
            {
                Debug.WriteLine(" - CellMouseDown: " + obj.ToString());
            }
            else
            {
                Debug.WriteLine(" - CellMouseDown: null ");
            }
        }

        private void dgvMerge_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            object obj = null;
            if (e.RowIndex > -1)
            {
                obj = dgvMerge.Rows[e.RowIndex].Cells[5].Value;
            }

            if (obj != null)
            {
                Debug.WriteLine(" - CellClick: " + obj.ToString());
            }
            else
            {
                Debug.WriteLine(" - CellClick: null ");
            }

            DataGridViewCheckBoxCell cell = this.dgvMerge.CurrentCell as DataGridViewCheckBoxCell;

            if (cell != null && !cell.ReadOnly)
            {
                cell.Value = cell.Value == null || !((bool)cell.Value);
                this.dgvMerge.RefreshEdit();
                this.dgvMerge.NotifyCurrentCellDirty(true);
            }

        }

        private void dgvMerge_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvMerge.RefreshEdit();
        }
    }
}