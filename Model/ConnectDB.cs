using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

using System.Data;

namespace Quan_ly_tai_nguyen_rung.Model
{
    public class ConnectDB
    {
        private static string sConnect = "Data Source = LY-HOAI-NAM; Initial Catalog = QL_TaiNguyenRung; Integrated Security = True; Trust Server Certificate = True";

        private static SqlConnection conn;

        public static void Dataconfig()
        {
            OpenConnect();
        }

        //Mở kết nối
        private static void OpenConnect()
        {
            conn = new SqlConnection(sConnect);
            conn.Open();
            if(conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }

        //Lấy dữ liệu sql về dưới dạng bảng bằng câu lệnh truy vấn
        private static DataTable DataTransport(string sSQL)
        {
            OpenConnect();

            // Khởi tạo adapter với câu truy vấn SQL và kết nối
            SqlDataAdapter adapter = new SqlDataAdapter(sSQL, conn);

            // Tạo đối tượng DataTable để lưu trữ kết quả
            DataTable dtReturn = new DataTable();

            // Làm sạch dữ liệu trong DataTable (nếu có)
            dtReturn.Clear();

            // Điền dữ liệu từ adapter vào DataTable
            adapter.Fill(dtReturn);

            // Trả về DataTable với kết quả truy vấn
            return dtReturn;
        }

        //Dùng để check xem việc thực thi lệnh SQL có thành công hay không
        private static int DataExecution(string sSQL)
        {
            int iResult = 0;

            // Mở kết nối nếu chưa mở
            OpenConnect();
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            // Khởi tạo SqlCommand và thiết lập thuộc tính
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sSQL;

            // Thực thi câu lệnh SQL và nhận kết quả
            iResult = cmd.ExecuteNonQuery();

            // Trả về số dòng bị ảnh hưởng
            return iResult;
        }


    }
}
