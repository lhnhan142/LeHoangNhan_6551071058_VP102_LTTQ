using LeHoangNhan_6551071058_VP102_LTTQ.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Linq;
using System.Windows.Forms;

namespace LeHoangNhan_6551071058_VP102_LTTQ
{
    public partial class Form1 : Form
    {
        DuLieuContext db = new DuLieuContext();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            var data = db.SinhViens.ToList();
            dataGridView1.DataSource = data;
            txtMSV.Clear();
            txtTenSV.Clear();
            txtDTB.Clear();
            CapNhatNut(false);
        }

        void CapNhatNut(bool daChon)
        {
            btnThem.Enabled = !daChon;
            btnSua.Enabled = daChon;
            btnXoa.Enabled = daChon;
            txtMSV.ReadOnly = daChon;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (KiemTraHopLe())
            {
                if (db.SinhViens.Any(s => s.MaSinhVien == txtMSV.Text))
                {
                    MessageBox.Show("Mã sinh viên này đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var sv = new SinhVien
                {
                    MaSinhVien = txtMSV.Text,
                    TenSinhVien = txtTenSV.Text,
                    DiemTrungBinh = txtDTB.Text
                };
                db.SinhViens.Add(sv);
                db.SaveChanges();
                MessageBox.Show("Thêm thành công!");
                LoadData();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {

                if (dataGridView1.CurrentRow.DataBoundItem is SinhVien sv)
                {
                    txtMSV.Text = sv.MaSinhVien;
                    txtTenSV.Text = sv.TenSinhVien;
                    txtDTB.Text = sv.DiemTrungBinh?.ToString();
                    CapNhatNut(true);
                }
            }
        }

        bool KiemTraHopLe()
        {
            if (string.IsNullOrWhiteSpace(txtMSV.Text))
            {
                MessageBox.Show("Mã sinh viên không được để trống");
                txtMSV.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenSV.Text))
            {
                MessageBox.Show("Tên sinh viên không được để trống");
                txtTenSV.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDTB.Text) || !double.TryParse(txtDTB.Text, out _))
            {
                MessageBox.Show("Điểm trung bình không được để trống và phải là số");
                txtDTB.Focus();
                return false;
            }
            return true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var sv = db.SinhViens.FirstOrDefault(s => s.MaSinhVien == txtMSV.Text);
                if (sv != null)
                {
                    db.SinhViens.Remove(sv);
                    db.SaveChanges();
                    MessageBox.Show("Xóa thành công!");
                    LoadData();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (KiemTraHopLe())
            {
                var sv = db.SinhViens.FirstOrDefault(s => s.MaSinhVien == txtMSV.Text);
                if (sv != null)
                {
                    sv.TenSinhVien = txtTenSV.Text;
                    sv.DiemTrungBinh = txtDTB.Text;

                    db.SaveChanges();
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên để sửa!");
                }
            }
        }
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}