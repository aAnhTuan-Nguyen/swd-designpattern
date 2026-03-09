# 🔌 Adapter Pattern — Giải thích chi tiết

## 📖 Adapter Pattern là gì?

**Adapter Pattern** là một **Structural Design Pattern** (mẫu thiết kế cấu trúc).
Nó cho phép các class có **interface không tương thích** có thể **làm việc cùng nhau**,
bằng cách tạo ra một lớp trung gian (Adapter) dịch interface này sang interface kia.

> 🔌 **Hình ảnh dễ hiểu:** Giống như chiếc **ổ cắm chuyển đổi điện** khi bạn đi du lịch —
> phích cắm Việt Nam (2 chân dẹt) không vừa ổ cắm châu Âu (2 chân tròn),
> nhưng bạn không cần mua thiết bị mới. Bạn chỉ cần một **adapter** để dịch hình dạng chân cắm.
> Adapter chính là **cầu nối** giữa hai interface không tương thích.

---

## 🏗️ Cấu trúc dự án

```
AdapterPattern/
├── Target/
│   └── INotificationSender.cs   ← ⭐ TARGET — Interface chuẩn client sử dụng
├── Adaptees/
│   ├── EmailService.cs          ← ADAPTEE 1 — Class cũ gửi email (API khác)
│   └── SmsService.cs            ← ADAPTEE 2 — Class cũ gửi SMS (API khác)
├── Adapters/
│   ├── EmailAdapter.cs          ← ADAPTER 1 — Bọc EmailService vào INotificationSender
│   └── SmsAdapter.cs            ← ADAPTER 2 — Bọc SmsService vào INotificationSender
├── Program.cs                   ← Demo so sánh có/không có Adapter
└── AdapterPattern.md            ← File giải thích bạn đang đọc
```

---

## 🧩 Các thành phần của Adapter Pattern

Adapter Pattern gồm **4 thành phần** chính:

| Thành phần | Class trong dự án | Vai trò |
|---|---|---|
| **Target** | `INotificationSender` | Interface chuẩn mà client kỳ vọng |
| **Adaptee** | `EmailService`, `SmsService` | Class cũ có interface không tương thích |
| **Adapter** | `EmailAdapter`, `SmsAdapter` | Cầu nối — implement Target, bên trong dùng Adaptee |
| **Client** | `Program.cs` | Chỉ biết Target interface, không biết Adaptee |

---

## 🔍 Giải thích từng thành phần

### 1. Target Interface (`INotificationSender`)

Interface chuẩn mà **toàn bộ hệ thống** sẽ dùng. Client chỉ biết interface này:

```csharp
public interface INotificationSender
{
    void Send(string recipient, string message);
}
```

Đơn giản, chỉ 1 phương thức với 2 tham số.

---

### 2. Adaptees — Các class cũ không tương thích

Đây là những class **đã tồn tại**, có API **khác với Target**. Ta không thể (hoặc không muốn) sửa chúng:

**`EmailService`** — nhận 3 tham số riêng biệt:
```csharp
// API của EmailService — KHÁC với INotificationSender
public void SendEmail(string toAddress, string subject, string body)
```

**`SmsService`** — tên phương thức và tham số khác hoàn toàn:
```csharp
// API của SmsService — KHÁC với INotificationSender
public void SendTextMessage(string phoneNumber, string text)
```

> ⚠️ Cả hai class này **không thể dùng trực tiếp** ở chỗ cần `INotificationSender`.

---

### 3. Adapters — Lớp chuyển đổi ⭐

Adapter là trái tim của pattern. Nó:
- **Implement** `INotificationSender` (để client có thể dùng)
- **Chứa** một instance của Adaptee (để gọi logic thực)
- **Dịch** lệnh từ Target API → Adaptee API

**`EmailAdapter`:**
```csharp
public class EmailAdapter : INotificationSender   // ← Implement Target
{
    private readonly EmailService _emailService;  // ← Chứa Adaptee

    public void Send(string recipient, string message)
    {
        // Dịch: 2 tham số chuẩn → 3 tham số của EmailService
        _emailService.SendEmail(
            toAddress: recipient,
            subject: "Thông báo hệ thống",
            body: message
        );
    }
}
```

**`SmsAdapter`:**
```csharp
public class SmsAdapter : INotificationSender     // ← Implement Target
{
    private readonly SmsService _smsService;      // ← Chứa Adaptee

    public void Send(string recipient, string message)
    {
        // Dịch: Send() → SendTextMessage()
        _smsService.SendTextMessage(
            phoneNumber: recipient,
            text: message
        );
    }
}
```

---

## ⚔️ So sánh: Có vs Không có Adapter

### ❌ KHÔNG dùng Adapter — Client bị ràng buộc với từng service

```csharp
var emailService = new EmailService();
var smsService   = new SmsService();

// Gửi email — phải biết đúng tên method và số tham số của EmailService
emailService.SendEmail("khach@gmail.com", "Thông báo hệ thống", "Đơn hàng đã xác nhận.");

// Gửi SMS — phải biết đúng tên method và số tham số của SmsService
smsService.SendTextMessage("0901234567", "Đơn hàng đã xác nhận.");
```

**Vấn đề:**
- ❌ Client bị **phụ thuộc trực tiếp** vào `EmailService` và `SmsService`
- ❌ **Không thể xử lý chung** trong một vòng lặp hay danh sách
- ❌ Muốn **thêm kênh mới** (Zalo, Push Notification...) phải sửa toàn bộ code client
- ❌ **Khó test** vì client gắn chặt với implementation cụ thể

---

### ✅ CÓ dùng Adapter — Client chỉ biết interface chuẩn

```csharp
// Tạo adapter — bọc service cũ vào interface chuẩn
INotificationSender emailSender = new EmailAdapter(new EmailService());
INotificationSender smsSender   = new SmsAdapter(new SmsService());

// Client làm việc với danh sách interface — đa hình hoàn toàn
var channels = new List<(INotificationSender sender, string recipient)>
{
    (emailSender, "khach@gmail.com"),
    (smsSender,   "0901234567"),
    (emailSender, "admin@company.com"),
};

// Cùng một đoạn code xử lý mọi loại kênh
foreach (var (sender, recipient) in channels)
{
    sender.Send(recipient, "Đơn hàng đã xác nhận.");
}
```

**Lợi ích:**
- ✅ Client **không biết** và **không cần biết** loại service bên trong
- ✅ Dễ dàng **xử lý chung** nhiều kênh trong một vòng lặp
- ✅ Thêm kênh mới chỉ cần **tạo Adapter mới**, không sửa code client (**Open/Closed Principle**)
- ✅ Dễ **mock/test** vì client chỉ phụ thuộc vào interface

---

## 🆚 So sánh Adapter vs Facade

Cả hai đều là **Structural Pattern** nhưng giải quyết vấn đề khác nhau:

| Tiêu chí | Adapter Pattern | Facade Pattern |
|---|---|---|
| **Mục đích** | Làm 2 interface không tương thích hoạt động cùng nhau | Đơn giản hóa interface phức tạp |
| **Bài toán** | "Class này có API sai, tôi không thể sửa nó" | "Hệ thống này quá phức tạp, client cần API đơn giản hơn" |
| **Kết quả** | Interface **tương thích** hơn | Interface **đơn giản** hơn |
| **Thay đổi interface?** | ✅ Có — dịch từ interface A → interface B | ❌ Không — chỉ gom nhiều bước vào 1 bước |
| **Hình ảnh** | Ổ cắm chuyển đổi điện | Remote thông minh "Xem phim" |

---

## 📐 Sơ đồ hoạt động

```
┌──────────┐      Send()        ┌──────────────┐    SendEmail()    ┌──────────────┐
│          │ ─────────────────► │ EmailAdapter │ ────────────────► │ EmailService │
│          │                    └──────────────┘                    └──────────────┘
│  Client  │      Send()        ┌──────────────┐  SendTextMessage() ┌────────────┐
│          │ ─────────────────► │  SmsAdapter  │ ────────────────► │ SmsService │
└──────────┘                    └──────────────┘                    └────────────┘

   Client chỉ biết Send()          Adapter "dịch"           Service thực thi
   (Target Interface)              sang API đúng             (Adaptee)
```

---

## 🕐 Khi nào nên dùng Adapter Pattern?

Dùng Adapter khi:
1. Bạn muốn dùng một **class đã có sẵn** nhưng interface của nó **không khớp** với hệ thống
2. Bạn muốn tích hợp **thư viện bên thứ 3** mà không muốn phụ thuộc trực tiếp vào nó
3. Bạn cần nhiều class **không liên quan** cùng thực hiện **một nhiệm vụ chung**
4. Bạn muốn **tái sử dụng** code cũ mà không cần viết lại
