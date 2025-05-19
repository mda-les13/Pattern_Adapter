// 1. Target - целевой интерфейс
public interface IPaymentProcessor
{
    void ProcessPayment(decimal amount);
}

// 2. Adaptee - старый класс с несовместимым интерфейсом
public class LegacyPaymentSystem
{
    public void MakeTransaction(string currency, double value)
    {
        Console.WriteLine($"Обработан платеж: {currency} {value}");
    }
}

// 3. Adapter - преобразует вызовы Target в вызовы Adaptee
public class PaymentAdapter : IPaymentProcessor
{
    private readonly LegacyPaymentSystem _legacySystem;

    public PaymentAdapter(LegacyPaymentSystem legacySystem)
    {
        _legacySystem = legacySystem;
    }

    public void ProcessPayment(decimal amount)
    {
        string currency = "USD";
        double convertedAmount = Convert.ToDouble(amount);
        _legacySystem.MakeTransaction(currency, convertedAmount);
    }
}

// 4. Client - клиентский код, использующий Target
public class ShoppingCart
{
    private readonly IPaymentProcessor _paymentProcessor;

    public ShoppingCart(IPaymentProcessor paymentProcessor)
    {
        _paymentProcessor = paymentProcessor;
    }

    public void Checkout(decimal totalAmount)
    {
        Console.WriteLine("Начинаем оформление заказа...");
        _paymentProcessor.ProcessPayment(totalAmount);
        Console.WriteLine("Заказ оформлен.");
    }
}

// Пример использования
class Program
{
    static void Main()
    {
        // Создаём старую систему и адаптируем её под новый интерфейс
        var legacySystem = new LegacyPaymentSystem();
        IPaymentProcessor adapter = new PaymentAdapter(legacySystem);

        // Клиент работает с целевым интерфейсом
        var cart = new ShoppingCart(adapter);
        cart.Checkout(150.75m);
    }
}

/*Результат работы программы:
Начинаем оформление заказа...
Обработан платеж: USD 150.75
Заказ оформлен.*/
