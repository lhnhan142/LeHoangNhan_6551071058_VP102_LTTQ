create database DuLieu
go

use DuLieu;

create table SinhVien(
MaSinhVien nvarchar(10) primary key,
TenSinhVien ntext not null,
DiemTrungBinh nvarchar(10) not null
);
go

insert into SinhVien values
('SV01', N'Lê Hoàng Nhân', '6.7'),
('SV02', N'Nguyễn Văn A', '1');

go
select * from SinhVien