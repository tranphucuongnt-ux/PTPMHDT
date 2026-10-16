using System;

namespace QuanLyCauLacBo
{
    
    public class ThanhVien
    {
        public int MaSo { get; private set; }
        public string HoTen { get; private set; }
        public string VaiTro { get; private set; }

       
        private ThanhVien(int maSo, string hoTen, string vaiTro)
        {
            MaSo = maSo;
            HoTen = hoTen;
            VaiTro = vaiTro;
        }

      
        public static ThanhVien Create(int maSo, string hoTen, string vaiTro)
        {
            if (string.IsNullOrWhiteSpace(hoTen))
                throw new ArgumentException("Họ tên không được để trống.");
            if (string.IsNullOrWhiteSpace(vaiTro))
                throw new ArgumentException("Vai trò không được để trống.");

            return new ThanhVien(maSo, hoTen, vaiTro);
        }
    }

    
    public class ThietBi
    {
        public int MaSo { get; private set; }
        public string TenThietBi { get; private set; }
        public string TrangThai { get; private set; }

        private ThietBi(int maSo, string tenThietBi, string trangThai)
        {
            MaSo = maSo;
            TenThietBi = tenThietBi;
            TrangThai = trangThai;
        }

        public static ThietBi Create(int maSo, string tenThietBi)
        {
            if (string.IsNullOrWhiteSpace(tenThietBi))
                throw new ArgumentException("Tên thiết bị không được để trống.");

            return new ThietBi(maSo, tenThietBi, "Available");
        }

        public void CapNhatTrangThai(string trangThaiMoi)
        {
            TrangThai = trangThaiMoi;
        }
    }

    
    public class PhieuMuon
    {
        public int MaSo { get; private set; }
        public int MaThanhVien { get; private set; }
        public int MaThietBi { get; private set; }
        public string TrangThai { get; private set; }

        private PhieuMuon(int maSo, int maThanhVien, int maThietBi, string trangThai)
        {
            MaSo = maSo;
            MaThanhVien = maThanhVien;
            MaThietBi = maThietBi;
            TrangThai = trangThai;
        }

        public static PhieuMuon Create(int maSo, int maThanhVien, int maThietBi)
        {
            if (maThanhVien <= 0 || maThietBi <= 0)
                throw new ArgumentException("Mã thành viên hoặc thiết bị không hợp lệ.");

            return new PhieuMuon(maSo, maThanhVien, maThietBi, "Waiting");
        }
    }

    
    public class PhanCongNhiemVu
    {
        public int MaSo { get; private set; }
        public int MaThanhVien { get; private set; }
        public string MoTaNhiemVu { get; private set; }
        public string TrangThai { get; private set; }

        private PhanCongNhiemVu(int maSo, int maThanhVien, string moTaNhiemVu, string trangThai)
        {
            MaSo = maSo;
            MaThanhVien = maThanhVien;
            MoTaNhiemVu = moTaNhiemVu;
            TrangThai = trangThai;
        }

        public static PhanCongNhiemVu Create(int maSo, int maThanhVien, string moTaNhiemVu)
        {
            if (string.IsNullOrWhiteSpace(moTaNhiemVu))
                throw new ArgumentException("Mô tả nhiệm vụ không được để trống.");

            return new PhanCongNhiemVu(maSo, maThanhVien, moTaNhiemVu, "InProgress");
        }
    }

    
    public class GiaoDichTaiChinh
    {
        public int MaSo { get; private set; }
        public decimal SoTien { get; private set; }
        public string GhiChu { get; private set; }

        private GiaoDichTaiChinh(int maSo, decimal soTien, string ghiChu)
        {
            MaSo = maSo;
            SoTien = soTien;
            GhiChu = ghiChu;
        }

        public static GiaoDichTaiChinh Create(int maSo, decimal soTien, string ghiChu)
        {
            if (soTien == 0)
                throw new ArgumentException("Số tiền giao dịch phải khác 0.");
            if (string.IsNullOrWhiteSpace(ghiChu))
                throw new ArgumentException("Ghi chú không được để trống.");

            return new GiaoDichTaiChinh(maSo, soTien, ghiChu);
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== HỆ THỐNG QUẢN LÝ CÂU LẠC BỘ ===\n");

            try
            {
                
                var svCuong = ThanhVien.Create(101, "Trần Phú Cường", "Sinh viên");
                var adminTrinh = ThanhVien.Create(999, "Admin Trinh", "Ban Chủ Nhiệm");
                Console.WriteLine($"[Hệ thống] Đã thêm: {svCuong.HoTen} ({svCuong.VaiTro})");
                Console.WriteLine($"[Hệ thống] Đã thêm: {adminTrinh.HoTen} ({adminTrinh.VaiTro})");

               
                var mayAnh = ThietBi.Create(1, "Máy ảnh Canon");
                var loaKeo = ThietBi.Create(2, "Loa di động");
                Console.WriteLine($"[Hệ thống] Đã thêm thiết bị: {mayAnh.TenThietBi} - Trạng thái: {mayAnh.TrangThai}");

               
                Console.WriteLine("\n[Nghiệp vụ 1] Sinh viên yêu cầu mượn thiết bị...");
                var phieuMuon = PhieuMuon.Create(1, svCuong.MaSo, mayAnh.MaSo);
                mayAnh.CapNhatTrangThai("Borrowed");
                Console.WriteLine($"- Lập phiếu mượn số {phieuMuon.MaSo}. Trạng thái: {phieuMuon.TrangThai}");

               
                Console.WriteLine("\n[Nghiệp vụ 2] Ban CLB phân công nhiệm vụ...");
                var nhiemVu = PhanCongNhiemVu.Create(1, svCuong.MaSo, "Hỗ trợ sự kiện chào tân sinh viên");
                Console.WriteLine($"- Giao nhiệm vụ: '{nhiemVu.MoTaNhiemVu}' cho sinh viên {svCuong.HoTen}. Trạng thái: {nhiemVu.TrangThai}");

                
                Console.WriteLine("\n[Nghiệp vụ 3] Cập nhật ngân sách CLB...");
                var thuTien = GiaoDichTaiChinh.Create(1, 500000, "Thu quỹ tháng 10");
                Console.WriteLine($"- Giao dịch mới: +{thuTien.SoTien:N0} VND ({thuTien.GhiChu}).");

                Console.WriteLine("\n=========================================");
                Console.WriteLine("      BÁO CÁO THAM DỰ & HOẠT ĐỘNG");
                Console.WriteLine("=========================================");
                Console.WriteLine($"- Số lượng thiết bị khả dụng (Available): 1 (Loa di động)");
                Console.WriteLine($"- Số lượng thiết bị đang cho mượn (Borrowed): 1 (Máy ảnh Canon)");
                Console.WriteLine($"- Phiếu mượn đang chờ xử lý (Waiting): 1 phiếu");
                Console.WriteLine($"- Nhiệm vụ đang thực hiện (InProgress): 1 nhiệm vụ");
                Console.WriteLine($"- Tổng quỹ CLB hiện tại: {thuTien.SoTien:N0} VND");
                Console.WriteLine("=========================================\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[LỖI HỆ THỐNG]: {ex.Message}");
            }
        }
    }
}