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
OnlineShop.Application  (Hiển thị giao diện, **nếu viết theo MVC thì bỏ qua UseCases, nếu viết API thì bỏ qua Controller**)
│   ├───Controllers
│   ├───Helpers
│   ├───Middlewares
│   ├───Migrations (Thứ cần có để DB chạy được)

├───OnlineShop.Benchmark (Đo hiệu suất chức năng)
├───OnlineShop.Business (Chứa những thứ có thể tái sử dụng được chỉ trong dự án này)
├───OnlineShop.Core (Nơi chứa DB)
│   ├───Database
│   ├───ExceptionHandle
│   ├───Helpers
│   ├───Models
│   └───Schemas
│       └───Base
├───OnlineShop.Hubs (Realtime)
├───OnlineShop.Services (Tương tác với DB)
├───OnlineShop.UnitTest (Testing)
└───OnlineShop.Utils  (Nơi chứa những thứ có thể tái sử dụng được trong dự án khác)

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
