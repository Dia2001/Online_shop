# Online Shop README
Project xây dựng dựa trên kiến trúc Clean Architecture.

## Call API

- Run backend
- Mở cookie trình duyệt
- Sao chép `accessToken`
- Vào `http://localhost:5001/swagger/index.html`
- Mở Authorize
- Gõ `Bearer dan_access_token`
- Submit

## Project structure
![image](https://github.com/Dia2001/dia_online_shop/assets/88370983/83e8e010-62b2-40d3-bca0-ba9127a51c12)



## Cách chạy

- cd vào OnlineShop.Backend
- Vào terminal gõ `dotnet build`
- cd vào OnlineShop.Application
- Vào terminal gõ `dotnet run`

## Cách Migrations DB (thứ cần có để kết nối DB)

- Vào terminal gõ: `dotnet tool install --global dotnet-ef`
- Cách kiểm tra cài đặt thành công, gõ lệnh: `dotnet ef`
- Chỉnh sửa file `appsetings.json` trong folder OnlineShop.Application chỉnh sửa biến: `connectionString` ứng với chuỗi kết nối của mình
- cd OnlineShop.Application, vào terminal gõ: `dotnet ef migrations add TenBatKy` (TenBatKy tương ứng với database=TenBatKy ở bước trên)
- Chạy tiếp lệnh: `dotnet ef database update`
- 
## Chú ý

- Đảm bảo đã cài môi trường `MySQL` và password trong `WorkBench` tương ứng password trong file `DataContext.cs`
- Để có dữ liệu vào: `http://localhost:5001/swagger/index.html`, scroll xuống cuối nhấn `api/seed` để có dữ liệu
- Nếu không login được FE thì chuyển sang nhánh Staging
