# 📚 Structural Design Patterns — Bilingual Reference

# 📚 Structural Design Patterns — Tài liệu Song ngữ

> **EN:** C# (.NET 8) · Facade Pattern · Adapter Pattern  
> **VI:** C# (.NET 8) · Facade Pattern · Adapter Pattern

---

## 📑 Table of Contents / Mục lục

| #   | Section / Phần                 | EN                                     | VI                                   |
| --- | ------------------------------ | -------------------------------------- | ------------------------------------ |
| 1   | What are Structural Patterns?  | [→](#-1-what-are-structural-patterns)  | [→](#-1-structural-patterns-là-gì)   |
| 2   | Facade Pattern                 | [→](#-2-facade-pattern--english)       | [→](#-2-facade-pattern--tiếng-việt)  |
| 3   | Adapter Pattern                | [→](#-3-adapter-pattern--english)      | [→](#-3-adapter-pattern--tiếng-việt) |
| 4   | Facade vs Adapter — Comparison | [→](#-4-facade-vs-adapter--comparison) | [→](#-4-facade-vs-adapter--so-sánh)  |
| 5   | Key Takeaways                  | [→](#-5-key-takeaways)                 | [→](#-5-kết-luận)                    |

---

---

# 🇬🇧 ENGLISH VERSION

---

## 🧭 1. What are Structural Patterns?

**Structural Design Patterns** are a category of design patterns that deal with
**how classes and objects are composed** to form larger, more flexible structures.

They do **not** change what a class does — they change **how classes connect to each other**.

There are 7 classic Structural Patterns (Gang of Four):

| Pattern     | One-line summary                                           |
| ----------- | ---------------------------------------------------------- |
| **Facade**  | Hide complexity behind a simple interface                  |
| **Adapter** | Make incompatible interfaces work together                 |
| Decorator   | Add responsibilities to objects dynamically                |
| Proxy       | Control access to another object                           |
| Bridge      | Separate abstraction from implementation                   |
| Composite   | Treat individual objects and groups uniformly              |
| Flyweight   | Share data to support large numbers of objects efficiently |

> This document focuses on **Facade** and **Adapter** — the two most commonly used in everyday development.

---

## 🎭 2. Facade Pattern — English

### What is it?

The **Facade Pattern** provides a **single, simple interface** to a **complex subsystem**
made up of many classes and many processing steps.

> 🏠 **Easy analogy:** Like pressing the **"Watch Movie"** button on a smart remote —
> you don't manually turn on the TV, switch to HDMI, set brightness, power on the speakers,
> set surround mode, adjust volume, start the player and press play.
> The remote does it all. **The remote is the Facade.**

---

### 📁 Project Structure

```
FacadePattern/
├── SubSystems/
│   ├── Television.cs        ← Controls the TV
│   ├── SoundSystem.cs       ← Controls audio
│   ├── StreamingPlayer.cs   ← Controls the media player
│   └── RoomLights.cs        ← Controls room lighting
├── Facade/
│   └── HomeTheaterFacade.cs ← ⭐ THE FACADE — simple unified interface
├── Program.cs               ← Demo: with vs without Facade
└── FacadePattern.md
```

---

### 🧩 Components

| Component      | Class                                                        | Role                                         |
| -------------- | ------------------------------------------------------------ | -------------------------------------------- |
| **Subsystems** | `Television`, `SoundSystem`, `StreamingPlayer`, `RoomLights` | Existing complex classes, each independent   |
| **Facade**     | `HomeTheaterFacade`                                          | Wraps all subsystems, exposes simple methods |
| **Client**     | `Program.cs`                                                 | Only talks to the Facade                     |

---

### ❌ Without Facade — The Problem

The client must know **every subsystem** and call them in the **exact right order**:

```csharp
// START watching — client must handle 9 individual steps
lights.Dim(10);
tv.TurnOn();
tv.SetInput("HDMI 1");
tv.SetBrightness(80);
sound.TurnOn();
sound.SetSurroundMode();
sound.SetVolume(60);
player.TurnOn();
player.Play("Avengers: Endgame");

// END watching — 5 more manual steps
player.Stop();
player.TurnOff();
sound.TurnOff();
tv.TurnOff();
lights.TurnOn();
```

**Problems:**

- 😩 Client must **know every device** in detail
- 😩 Client must **remember the correct order** of operations
- 😩 **Duplicated code** every time you want to watch a movie
- 😩 Adding a new device means **updating every single client**
- 😩 **Tight coupling** — the client is entangled with all subsystems

---

### ✅ With Facade — The Solution

The `HomeTheaterFacade` wraps all 9 (and 5) steps inside two clean methods:

```csharp
var homeTheater = new HomeTheaterFacade(tv, sound, player, lights);

homeTheater.WatchMovie("Avengers: Endgame"); // ← ONE call starts everything
homeTheater.EndMovie();                       // ← ONE call stops everything
```

**How `WatchMovie()` works internally — the Facade still calls all 9 steps,
but now the client never sees them:**

```csharp
public void WatchMovie(string movie)
{
    _lights.Dim(10);
    _tv.TurnOn();
    _tv.SetInput("HDMI 1");
    _tv.SetBrightness(80);
    _sound.TurnOn();
    _sound.SetSurroundMode();
    _sound.SetVolume(60);
    _player.TurnOn();
    _player.Play(movie);
}
```

**Benefits:**

- ✅ Client code is **clean** — 1 line replaces 9
- ✅ Adding a new device = only **modify the Facade**, not every client
- ✅ **Loose coupling** — client depends only on `HomeTheaterFacade`
- ✅ Logic is centralized — **one place to maintain**

---

### 📊 Comparison Table

| Criteria                     | Without Facade      | With Facade              |
| ---------------------------- | ------------------- | ------------------------ |
| Lines of client code per use | ~10 lines each time | 1 line each time         |
| Client knows subsystems?     | ✅ Must know all    | ❌ Knows nothing         |
| Add new device               | Edit every client   | Edit Facade only         |
| Code reuse                   | Hard (copy-paste)   | Easy (call Facade again) |
| Coupling                     | Tight               | Loose                    |
| Initial setup complexity     | Low                 | Slightly higher          |

---

### 📐 Relationship Diagram

```
┌─────────────────────────────────────────┐
│              CLIENT (Program.cs)        │
└──────────────────┬──────────────────────┘
                   │ calls only Facade
                   ▼
┌─────────────────────────────────────────┐
│          HomeTheaterFacade              │
│   WatchMovie(movie)  /  EndMovie()      │
└──┬───────────┬────────────┬─────────────┘
   │           │            │           └──► RoomLights
   ▼           ▼            ▼
Television  SoundSystem  StreamingPlayer
```

---

### 🕐 When to use Facade?

✅ When a subsystem has **many complex classes** interacting with each other  
✅ When you want to give clients a **simple entry point**  
✅ When you want to **reduce dependencies** between client and subsystem  
✅ When you want to **layer** your application architecture

---

---

## 🔌 3. Adapter Pattern — English

### What is it?

The **Adapter Pattern** allows classes with **incompatible interfaces work together**
by introducing a **middle-layer class** (the Adapter) that converts one interface into another.

> 🔌 **Easy analogy:** Like a **travel power plug adapter** —
> your Vietnamese plug (2 flat pins) does not fit a European socket (2 round pins).
> You don't buy a new device — you use an **adapter** to bridge the physical gap.
> **The adapter translates the shape.**

---

### 📁 Project Structure

```
AdapterPattern/
├── Target/
│   └── INotificationSender.cs   ← ⭐ TARGET — standard interface the client uses
├── Adaptees/
│   ├── EmailService.cs          ← ADAPTEE 1 — legacy email class (different API)
│   └── SmsService.cs            ← ADAPTEE 2 — legacy SMS class (different API)
├── Adapters/
│   ├── EmailAdapter.cs          ← ADAPTER 1 — wraps EmailService into INotificationSender
│   └── SmsAdapter.cs            ← ADAPTER 2 — wraps SmsService into INotificationSender
├── Program.cs                   ← Demo: with vs without Adapter
└── AdapterPattern.md
```

---

### 🧩 Components

| Component   | Class                        | Role                                                |
| ----------- | ---------------------------- | --------------------------------------------------- |
| **Target**  | `INotificationSender`        | Standard interface the client expects               |
| **Adaptee** | `EmailService`, `SmsService` | Existing classes with incompatible APIs             |
| **Adapter** | `EmailAdapter`, `SmsAdapter` | Bridge — implements Target, uses Adaptee internally |
| **Client**  | `Program.cs`                 | Only knows the Target interface                     |

---

### 🔍 The incompatibility problem

The client wants a **simple, unified interface**:

```csharp
// What the client wants to call — TARGET interface
void Send(string recipient, string message);   // 2 parameters, one method name
```

But the existing services have **completely different signatures**:

```csharp
// EmailService — DIFFERENT: 3 parameters, different name
void SendEmail(string toAddress, string subject, string body);

// SmsService — DIFFERENT: different method name entirely
void SendTextMessage(string phoneNumber, string text);
```

Neither can be used directly where `INotificationSender` is expected.

---

### ❌ Without Adapter — The Problem

The client is forced to call each service using its own specific API:

```csharp
var emailService = new EmailService();
var smsService   = new SmsService();

// Must know EmailService's exact method name and 3-parameter signature
emailService.SendEmail("customer@gmail.com", "System Notification", "Order #1001 confirmed.");

// Must know SmsService's completely different method name
smsService.SendTextMessage("0901234567", "Order #1001 confirmed.");
```

**Problems:**

- ❌ Client is **directly coupled** to `EmailService` and `SmsService`
- ❌ **Cannot process both in a single loop** — no common interface
- ❌ Adding a new channel (Zalo, Push...) = **rewriting client code**
- ❌ **Hard to unit test** — cannot inject mock implementations

---

### ✅ With Adapter — The Solution

Each Adapter **implements** `INotificationSender` (so it looks like the Target to the client)
and **internally wraps** the real service (Adaptee):

**`EmailAdapter` — translates `Send()` → `SendEmail()`:**

```csharp
public class EmailAdapter : INotificationSender   // implements Target ← client sees this
{
    private readonly EmailService _emailService;  // holds Adaptee ← real work done here

    public void Send(string recipient, string message)
    {
        // TRANSLATION: 2 standard params → 3 EmailService params
        _emailService.SendEmail(
            toAddress: recipient,
            subject: "System Notification",
            body: message
        );
    }
}
```

**`SmsAdapter` — translates `Send()` → `SendTextMessage()`:**

```csharp
public class SmsAdapter : INotificationSender
{
    private readonly SmsService _smsService;

    public void Send(string recipient, string message)
    {
        // TRANSLATION: Send() → SendTextMessage()
        _smsService.SendTextMessage(
            phoneNumber: recipient,
            text: message
        );
    }
}
```

**Client code — clean, uniform, and extensible:**

```csharp
INotificationSender emailSender = new EmailAdapter(new EmailService());
INotificationSender smsSender   = new SmsAdapter(new SmsService());

// Works as a unified list — pure polymorphism
var channels = new List<(INotificationSender sender, string recipient)>
{
    (emailSender, "customer@gmail.com"),
    (smsSender,   "0901234567"),
    (emailSender, "admin@company.com"),
};

// ONE loop handles ALL channels — regardless of the underlying service
foreach (var (sender, recipient) in channels)
{
    sender.Send(recipient, "Order #1002 confirmed.");
}
```

**Benefits:**

- ✅ Client only calls `Send()` — **knows nothing** about the underlying service
- ✅ All channels **processed uniformly** in one loop
- ✅ Adding a new channel = **create a new Adapter only** — client untouched
  _(this is the **Open/Closed Principle** — open for extension, closed for modification)_
- ✅ Easy to **mock and unit test** — just inject a fake `INotificationSender`

---

### 📊 Comparison Table

| Criteria                        | Without Adapter              | With Adapter              |
| ------------------------------- | ---------------------------- | ------------------------- |
| Client knows service internals? | ✅ Must know method names    | ❌ Knows only `Send()`    |
| Uniform processing (loop)?      | ❌ Impossible                | ✅ One loop for all       |
| Add new channel                 | Rewrite client code          | Add new Adapter only      |
| Coupling                        | Tight (to specific services) | Loose (to interface only) |
| Unit testability                | Hard                         | Easy (mock the interface) |
| SOLID principle                 | ❌ Violates OCP              | ✅ Open/Closed + DIP      |

---

### 📐 Flow Diagram

```
          CLIENT
            │  .Send(recipient, message)   ← ONE interface for everything
            │
      ┌─────┴──────┐
      ▼            ▼
EmailAdapter   SmsAdapter              ← Adapter translates the call
      │              │
      │ .SendEmail() │ .SendTextMessage()  ← Adaptee's actual API
      ▼              ▼
EmailService   SmsService              ← Real work happens here
```

---

### 🕐 When to use Adapter?

✅ When you want to use an **existing class** but its interface **doesn't match** your system  
✅ When integrating a **third-party library** without directly depending on it  
✅ When multiple **unrelated classes** need to perform the **same task**  
✅ When you want to **reuse legacy code** without rewriting it

---

---

---

# 🇻🇳 PHIÊN BẢN TIẾNG VIỆT

---

## 🧭 1. Structural Patterns là gì?

**Structural Design Pattern** (Mẫu thiết kế cấu trúc) là nhóm pattern xử lý việc
**các class và object kết hợp với nhau** để tạo thành cấu trúc lớn hơn, linh hoạt hơn.

Chúng **không** thay đổi class làm gì — chúng thay đổi **cách các class kết nối với nhau**.

Có 7 Structural Pattern cổ điển (Gang of Four):

| Pattern     | Tóm tắt một câu                                        |
| ----------- | ------------------------------------------------------ |
| **Facade**  | Ẩn sự phức tạp sau một interface đơn giản              |
| **Adapter** | Làm 2 interface không tương thích hoạt động cùng nhau  |
| Decorator   | Thêm chức năng vào object một cách linh hoạt           |
| Proxy       | Kiểm soát truy cập vào một object khác                 |
| Bridge      | Tách abstraction khỏi implementation                   |
| Composite   | Xử lý đồng nhất đối tượng đơn lẻ và nhóm               |
| Flyweight   | Chia sẻ dữ liệu để hỗ trợ số lượng lớn object hiệu quả |

> Tài liệu này tập trung vào **Facade** và **Adapter** — hai pattern phổ biến nhất trong phát triển hàng ngày.

---

## 🎭 2. Facade Pattern — Tiếng Việt

### Là gì?

**Facade Pattern** cung cấp một **giao diện đơn giản duy nhất** cho một **hệ thống con phức tạp**
gồm nhiều class và nhiều bước xử lý.

> 🏠 **Hình ảnh dễ hiểu:** Giống như bấm nút **"Xem phim"** trên remote thông minh —
> bạn không cần tự bật TV, chỉnh HDMI, chỉnh độ sáng, bật loa,
> chỉnh chế độ âm vòm, chỉnh âm lượng, bật đầu phát rồi nhấn play.
> Remote làm hết tất cả. **Remote chính là Facade.**

---

### 📁 Cấu trúc dự án

```
FacadePattern/
├── SubSystems/
│   ├── Television.cs        ← Điều khiển TV
│   ├── SoundSystem.cs       ← Điều khiển âm thanh
│   ├── StreamingPlayer.cs   ← Điều khiển trình phát phim
│   └── RoomLights.cs        ← Điều khiển đèn phòng
├── Facade/
│   └── HomeTheaterFacade.cs ← ⭐ LỚP FACADE — giao diện đơn giản thống nhất
├── Program.cs               ← Demo so sánh có/không có Facade
└── FacadePattern.md
```

---

### 🧩 Các thành phần

| Thành phần     | Class                                                        | Vai trò                                             |
| -------------- | ------------------------------------------------------------ | --------------------------------------------------- |
| **Subsystems** | `Television`, `SoundSystem`, `StreamingPlayer`, `RoomLights` | Các class phức tạp đã tồn tại, hoạt động độc lập    |
| **Facade**     | `HomeTheaterFacade`                                          | Bọc tất cả subsystem, cung cấp phương thức đơn giản |
| **Client**     | `Program.cs`                                                 | Chỉ giao tiếp với Facade                            |

---

### ❌ Không dùng Facade — Vấn đề

Client phải biết **từng thiết bị** và gọi chúng theo **đúng thứ tự**:

```csharp
// BẮT ĐẦU xem phim — client phải xử lý 9 bước riêng lẻ
lights.Dim(10);
tv.TurnOn();
tv.SetInput("HDMI 1");
tv.SetBrightness(80);
sound.TurnOn();
sound.SetSurroundMode();
sound.SetVolume(60);
player.TurnOn();
player.Play("Avengers: Endgame");

// KẾT THÚC xem phim — thêm 5 bước thủ công
player.Stop();
player.TurnOff();
sound.TurnOff();
tv.TurnOff();
lights.TurnOn();
```

**Vấn đề:**

- 😩 Client phải **biết chi tiết từng thiết bị**
- 😩 Client phải **nhớ đúng thứ tự** thực hiện
- 😩 **Code lặp lại** mỗi lần muốn xem phim
- 😩 Thêm thiết bị mới → phải **sửa tất cả** code client
- 😩 **Khớp nối chặt** — client bị ràng buộc với mọi hệ thống con

---

### ✅ Có dùng Facade — Giải pháp

`HomeTheaterFacade` ẩn toàn bộ 9 (và 5) bước bên trong hai phương thức gọn gàng:

```csharp
var homeTheater = new HomeTheaterFacade(tv, sound, player, lights);

homeTheater.WatchMovie("Avengers: Endgame"); // ← MỘT lệnh — bật tất cả
homeTheater.EndMovie();                       // ← MỘT lệnh — tắt tất cả
```

**`WatchMovie()` hoạt động như thế nào bên trong — Facade vẫn gọi đủ 9 bước,
nhưng client không bao giờ thấy chúng:**

```csharp
public void WatchMovie(string movie)
{
    _lights.Dim(10);
    _tv.TurnOn();
    _tv.SetInput("HDMI 1");
    _tv.SetBrightness(80);
    _sound.TurnOn();
    _sound.SetSurroundMode();
    _sound.SetVolume(60);
    _player.TurnOn();
    _player.Play(movie);
}
```

**Lợi ích:**

- ✅ Code client **gọn gàng** — 1 dòng thay vì 9
- ✅ Thêm thiết bị mới = chỉ **sửa Facade**, không đụng client
- ✅ **Khớp nối lỏng** — client chỉ phụ thuộc `HomeTheaterFacade`
- ✅ Logic tập trung — **một nơi duy nhất** để bảo trì

---

### 📊 Bảng so sánh

| Tiêu chí                         | Không có Facade   | Có Facade           |
| -------------------------------- | ----------------- | ------------------- |
| Số dòng code client mỗi lần dùng | ~10 dòng          | 1 dòng              |
| Client cần biết subsystem?       | ✅ Phải biết hết  | ❌ Không cần biết   |
| Thêm thiết bị mới                | Sửa tất cả client | Chỉ sửa Facade      |
| Tái sử dụng                      | Khó (copy-paste)  | Dễ (gọi lại Facade) |
| Khớp nối (Coupling)              | Chặt (tight)      | Lỏng (loose)        |
| Độ phức tạp ban đầu              | Thấp              | Cao hơn một chút    |

---

### 📐 Sơ đồ quan hệ

```
┌───────────────────────────────────────┐
│          CLIENT (Program.cs)          │
└──────────────────┬────────────────────┘
                   │ chỉ gọi Facade
                   ▼
┌───────────────────────────────────────┐
│         HomeTheaterFacade             │
│   WatchMovie(movie)  /  EndMovie()    │
└──┬──────────┬───────────┬─────────────┘
   │          │           │         └──► RoomLights
   ▼          ▼           ▼
Television  SoundSystem  StreamingPlayer
```

---

### 🕐 Khi nào nên dùng Facade?

✅ Khi hệ thống có **nhiều class phức tạp** tương tác với nhau  
✅ Khi muốn cung cấp cho client **một điểm vào đơn giản**  
✅ Khi muốn **giảm sự phụ thuộc** giữa client và hệ thống con  
✅ Khi muốn **tổ chức ứng dụng thành các lớp** (layers)

---

---

## 🔌 3. Adapter Pattern — Tiếng Việt

### Là gì?

**Adapter Pattern** cho phép các class có **interface không tương thích hoạt động cùng nhau**
bằng cách đặt một lớp trung gian (Adapter) **dịch** interface này sang interface kia.

> 🔌 **Hình ảnh dễ hiểu:** Giống như **ổ cắm chuyển đổi điện** khi đi du lịch —
> phích cắm Việt Nam (2 chân dẹt) không vừa ổ cắm châu Âu (2 chân tròn).
> Bạn không mua thiết bị mới — bạn dùng **adapter** để kết nối.
> **Adapter dịch hình dạng.**

---

### 📁 Cấu trúc dự án

```
AdapterPattern/
├── Target/
│   └── INotificationSender.cs   ← ⭐ TARGET — interface chuẩn client sử dụng
├── Adaptees/
│   ├── EmailService.cs          ← ADAPTEE 1 — class cũ gửi email (API khác)
│   └── SmsService.cs            ← ADAPTEE 2 — class cũ gửi SMS (API khác)
├── Adapters/
│   ├── EmailAdapter.cs          ← ADAPTER 1 — bọc EmailService vào INotificationSender
│   └── SmsAdapter.cs            ← ADAPTER 2 — bọc SmsService vào INotificationSender
├── Program.cs                   ← Demo so sánh có/không có Adapter
└── AdapterPattern.md
```

---

### 🧩 Các thành phần

| Thành phần  | Class                        | Vai trò                                            |
| ----------- | ---------------------------- | -------------------------------------------------- |
| **Target**  | `INotificationSender`        | Interface chuẩn mà client kỳ vọng                  |
| **Adaptee** | `EmailService`, `SmsService` | Class cũ có API không tương thích                  |
| **Adapter** | `EmailAdapter`, `SmsAdapter` | Cầu nối — implement Target, dùng Adaptee bên trong |
| **Client**  | `Program.cs`                 | Chỉ biết Target interface                          |

---

### 🔍 Vấn đề không tương thích

Client muốn một **interface đơn giản, thống nhất**:

```csharp
// Thứ client muốn gọi — TARGET interface
void Send(string recipient, string message);   // 2 tham số, tên phương thức nhất quán
```

Nhưng các service hiện có lại có **chữ ký hoàn toàn khác**:

```csharp
// EmailService — KHÁC: 3 tham số, tên khác
void SendEmail(string toAddress, string subject, string body);

// SmsService — KHÁC: tên phương thức khác hoàn toàn
void SendTextMessage(string phoneNumber, string text);
```

Cả hai **không thể dùng trực tiếp** ở chỗ cần `INotificationSender`.

---

### ❌ Không dùng Adapter — Vấn đề

Client bị buộc phải gọi từng service theo API riêng của nó:

```csharp
var emailService = new EmailService();
var smsService   = new SmsService();

// Phải biết đúng tên method và chữ ký 3 tham số của EmailService
emailService.SendEmail("khach@gmail.com", "Thông báo hệ thống", "Đơn hàng #1001 đã xác nhận.");

// Phải biết tên method hoàn toàn khác của SmsService
smsService.SendTextMessage("0901234567", "Đơn hàng #1001 đã xác nhận.");
```

**Vấn đề:**

- ❌ Client **phụ thuộc trực tiếp** vào `EmailService` và `SmsService`
- ❌ **Không thể xử lý chung** trong một vòng lặp — không có interface chung
- ❌ Thêm kênh mới (Zalo, Push...) = phải **viết lại code client**
- ❌ **Khó unit test** — không thể inject implementation giả

---

### ✅ Có dùng Adapter — Giải pháp

Mỗi Adapter **implement** `INotificationSender` (để trông như Target với client)
và **bên trong bọc** service thực (Adaptee):

**`EmailAdapter` — dịch `Send()` → `SendEmail()`:**

```csharp
public class EmailAdapter : INotificationSender   // implement Target ← client thấy đây
{
    private readonly EmailService _emailService;  // chứa Adaptee ← công việc thực ở đây

    public void Send(string recipient, string message)
    {
        // DỊCH: 2 tham số chuẩn → 3 tham số của EmailService
        _emailService.SendEmail(
            toAddress: recipient,
            subject: "Thông báo hệ thống",
            body: message
        );
    }
}
```

**`SmsAdapter` — dịch `Send()` → `SendTextMessage()`:**

```csharp
public class SmsAdapter : INotificationSender
{
    private readonly SmsService _smsService;

    public void Send(string recipient, string message)
    {
        // DỊCH: Send() → SendTextMessage()
        _smsService.SendTextMessage(
            phoneNumber: recipient,
            text: message
        );
    }
}
```

**Code client — gọn gàng, thống nhất và dễ mở rộng:**

```csharp
INotificationSender emailSender = new EmailAdapter(new EmailService());
INotificationSender smsSender   = new SmsAdapter(new SmsService());

// Hoạt động như một danh sách thống nhất — đa hình hoàn toàn
var channels = new List<(INotificationSender sender, string recipient)>
{
    (emailSender, "khach@gmail.com"),
    (smsSender,   "0901234567"),
    (emailSender, "admin@company.com"),
};

// MỘT vòng lặp xử lý TẤT CẢ kênh — bất kể service bên trong là gì
foreach (var (sender, recipient) in channels)
{
    sender.Send(recipient, "Đơn hàng #1002 đã xác nhận.");
}
```

**Lợi ích:**

- ✅ Client chỉ gọi `Send()` — **không biết gì** về service bên trong
- ✅ Mọi kênh được **xử lý đồng nhất** trong một vòng lặp
- ✅ Thêm kênh mới = **chỉ tạo Adapter mới** — client không cần sửa
  _(đây là nguyên tắc **Open/Closed** — mở để mở rộng, đóng để sửa đổi)_
- ✅ Dễ **mock và unit test** — chỉ cần inject `INotificationSender` giả

---

### 📊 Bảng so sánh

| Tiêu chí                    | Không có Adapter        | Có Adapter                 |
| --------------------------- | ----------------------- | -------------------------- |
| Client biết nội bộ service? | ✅ Phải biết tên method | ❌ Chỉ biết `Send()`       |
| Xử lý chung (vòng lặp)?     | ❌ Không thể            | ✅ Một vòng lặp cho tất cả |
| Thêm kênh mới               | Phải viết lại client    | Chỉ thêm Adapter mới       |
| Khớp nối                    | Chặt (với từng service) | Lỏng (với interface)       |
| Khả năng unit test          | Khó                     | Dễ (mock interface)        |
| Nguyên tắc SOLID            | ❌ Vi phạm OCP          | ✅ Open/Closed + DIP       |

---

### 📐 Sơ đồ luồng

```
          CLIENT
            │  .Send(recipient, message)   ← MỘT interface cho tất cả
            │
      ┌─────┴──────┐
      ▼            ▼
EmailAdapter   SmsAdapter              ← Adapter dịch lệnh
      │              │
      │ .SendEmail() │ .SendTextMessage()  ← API thực của Adaptee
      ▼              ▼
EmailService   SmsService              ← Công việc thực xảy ra ở đây
```

---

### 🕐 Khi nào nên dùng Adapter?

✅ Khi muốn dùng **class đã có sẵn** nhưng interface **không khớp** với hệ thống  
✅ Khi tích hợp **thư viện bên thứ 3** mà không muốn phụ thuộc trực tiếp  
✅ Khi nhiều class **không liên quan** cần thực hiện **cùng một nhiệm vụ**  
✅ Khi muốn **tái sử dụng code cũ** mà không cần viết lại

---

---

## ⚔️ 4. Facade vs Adapter — Comparison / So sánh

### English

| Question                            | Facade                                     | Adapter                                       |
| ----------------------------------- | ------------------------------------------ | --------------------------------------------- |
| **What problem does it solve?**     | Complexity — too many steps for the client | Incompatibility — wrong interface shape       |
| **Does it change the interface?**   | No — hides complexity behind one method    | Yes — converts interface A into interface B   |
| **How many classes does it wrap?**  | Many (a whole subsystem)                   | Usually one per Adapter                       |
| **Does the client know internals?** | No — fully hidden                          | No — hidden behind Target interface           |
| **Real-world analogy**              | "Watch Movie" smart remote button          | Travel power plug adapter                     |
| **Key benefit**                     | Simpler API for the client                 | Reuse existing classes without modifying them |
| **SOLID principle applied**         | Single Responsibility, Law of Demeter      | Open/Closed Principle, Dependency Inversion   |
| **Demo scenario**                   | Home Theater System                        | Notification Sending System                   |
| **Number of roles**                 | 2 (Facade + Subsystems)                    | 4 (Target + Adaptee + Adapter + Client)       |

---

### Tiếng Việt

| Câu hỏi                             | Facade                                  | Adapter                                       |
| ----------------------------------- | --------------------------------------- | --------------------------------------------- |
| **Giải quyết vấn đề gì?**           | Phức tạp — quá nhiều bước cho client    | Không tương thích — interface sai hình dạng   |
| **Có thay đổi interface không?**    | Không — ẩn phức tạp sau một phương thức | Có — chuyển đổi interface A thành interface B |
| **Bọc bao nhiêu class?**            | Nhiều (cả hệ thống con)                 | Thường một class mỗi Adapter                  |
| **Client có biết bên trong không?** | Không — ẩn hoàn toàn                    | Không — ẩn sau Target interface               |
| **Hình ảnh thực tế**                | Nút "Xem phim" trên remote thông minh   | Ổ cắm chuyển đổi điện khi đi du lịch          |
| **Lợi ích chính**                   | API đơn giản hơn cho client             | Tái sử dụng class cũ mà không sửa             |
| **Nguyên tắc SOLID**                | Single Responsibility, Law of Demeter   | Open/Closed, Dependency Inversion             |
| **Kịch bản demo**                   | Hệ thống rạp chiếu phim tại nhà         | Hệ thống gửi thông báo                        |
| **Số lượng vai trò**                | 2 (Facade + Subsystems)                 | 4 (Target + Adaptee + Adapter + Client)       |

---

---

## ✅ 5. Key Takeaways / Kết luận

### English

1. Both are **Structural Patterns** — they shape _how_ classes connect, not _what_ they do.
2. **Facade** = _"Make it simpler"_ — wrap a complex system behind a clean single interface.
3. **Adapter** = _"Make it compatible"_ — bridge two interfaces that cannot work together natively.
4. **Facade** wraps **many classes**; **Adapter** wraps **one class** per adapter.
5. **Facade** answers: _"How do I simplify this?"_; **Adapter** answers: _"How do I reuse this?"_
6. Both reduce **tight coupling**, improve **maintainability**, and make code easier to **extend and test**.

---

### Tiếng Việt

1. Cả hai đều là **Structural Pattern** — định hình _cách_ các class kết nối, không phải _những gì_ chúng làm.
2. **Facade** = _"Làm cho đơn giản hơn"_ — bọc hệ thống phức tạp sau một interface gọn gàng.
3. **Adapter** = _"Làm cho tương thích"_ — kết nối hai interface không thể tự làm việc cùng nhau.
4. **Facade** bọc **nhiều class**; **Adapter** bọc **một class** mỗi adapter.
5. **Facade** trả lời: _"Làm sao đơn giản hóa cái này?"_; **Adapter** trả lời: _"Làm sao tái sử dụng cái này?"_
6. Cả hai đều giảm **khớp nối chặt**, cải thiện **khả năng bảo trì**, giúp code dễ **mở rộng và kiểm thử** hơn.
