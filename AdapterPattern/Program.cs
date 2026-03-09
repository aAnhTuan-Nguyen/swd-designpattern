using AdapterPattern.Adaptees;
using AdapterPattern.Adapters;
using AdapterPattern.Target;

namespace AdapterPattern;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // ╔════════════════════════════════════════════════════╗
        // ║      PHẦN 1: KHÔNG SỬ DỤNG ADAPTER PATTERN       ║
        // ╚════════════════════════════════════════════════════╝
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║      KHÔNG SỬ DỤNG ADAPTER PATTERN               ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("Client phải gọi trực tiếp từng service theo API riêng của nó:\n");

        var emailService = new EmailService();
        var smsService = new SmsService();

        Console.WriteLine("--- Gửi thông báo đơn hàng #1001 ---\n");

        // Gửi email — phải biết đúng tên method và số tham số của EmailService
        emailService.SendEmail("khach@gmail.com", "Thông báo hệ thống", "Đơn hàng #1001 đã được xác nhận.");
        Console.WriteLine();

        // Gửi SMS — phải biết đúng tên method và số tham số của SmsService
        smsService.SendTextMessage("0901234567", "Đơn hàng #1001 đã được xác nhận.");

        Console.WriteLine();
        Console.WriteLine("⚠️  Vấn đề:");
        Console.WriteLine("    - Client bị phụ thuộc trực tiếp vào EmailService và SmsService.");
        Console.WriteLine("    - Không thể xử lý chung trong vòng lặp hay danh sách.");
        Console.WriteLine("    - Muốn thêm kênh mới (Zalo...) phải sửa toàn bộ code client.");

        Console.WriteLine("\n");

        // ╔════════════════════════════════════════════════════╗
        // ║      PHẦN 2: SỬ DỤNG ADAPTER PATTERN             ║
        // ╚════════════════════════════════════════════════════╝
        Console.WriteLine("╔════════════════════════════════════════════════════╗");
        Console.WriteLine("║      SỬ DỤNG ADAPTER PATTERN                     ║");
        Console.WriteLine("╚════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("Client chỉ cần biết INotificationSender — không quan tâm loại service:\n");

        // Adapter bọc các service cũ vào interface chuẩn
        INotificationSender emailSender = new EmailAdapter(new EmailService());
        INotificationSender smsSender   = new SmsAdapter(new SmsService());

        // Client làm việc với danh sách interface — đa hình hoàn toàn
        var channels = new List<(INotificationSender sender, string recipient)>
        {
            (emailSender, "khach@gmail.com"),
            (smsSender,   "0901234567"),
            (emailSender, "admin@company.com"),
        };

        Console.WriteLine("--- Gửi thông báo đơn hàng #1002 ---\n");
        foreach (var (sender, recipient) in channels)
        {
            sender.Send(recipient, "Đơn hàng #1002 đã được xác nhận.");
            Console.WriteLine();
        }
        //foreach (var channel in channels) {
        //    channel.sender.Send(channel.recipient, "Đơn hàng #1003 đã được xác nhận.");
        //    Console.WriteLine();
        //}
        Console.WriteLine("✅ Lợi ích:");
        Console.WriteLine("   - Client chỉ gọi Send() — không cần biết loại service bên trong.");
        Console.WriteLine("   - Dễ dàng xử lý chung nhiều kênh trong một vòng lặp.");
        Console.WriteLine("   - Thêm kênh mới chỉ cần tạo Adapter mới, không sửa code client.");
    }
}

