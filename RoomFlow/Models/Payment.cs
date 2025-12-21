using RoomFlow.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Payment
{
	public int Id { get; set; }

	[Required(ErrorMessage = "Выберите бронирование")]
	[Display(Name = "ID бронирования")]
	public int BookingId { get; set; }

	[Required(ErrorMessage = "Введите сумму оплаты")]
	[Column(TypeName = "decimal(10,2)")]
	[Range(0.01, 1000000, ErrorMessage = "Сумма оплаты должна быть больше 0")]
	[Display(Name = "Сумма оплаты (₽)")]
	public decimal Amount { get; set; }

	[Required(ErrorMessage = "Выберите способ оплаты")]
	[Display(Name = "Способ оплаты")]
	public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.CreditCard;

	[Required(ErrorMessage = "Выберите статус оплаты")]
	[Display(Name = "Статус оплаты")]
	public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

	[Required(ErrorMessage = "Введите дату оплаты")]
	[DataType(DataType.DateTime)]
	[Display(Name = "Дата и время оплаты")]
	public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

	[StringLength(50)]
	[Display(Name = "Номер транзакции")]
	public string? TransactionId { get; set; }

	[StringLength(500)]
	[Display(Name = "Примечания к оплате")]
	public string? Notes { get; set; }

	[Required(ErrorMessage = "Выберите валюту")]
	[StringLength(3)]
	[Display(Name = "Валюта")]
	public string Currency { get; set; } = "RUB";

	[Display(Name = "Курс валюты")]
	[Column(TypeName = "decimal(8,4)")]
	public decimal ExchangeRate { get; set; } = 1.0m;

	[Display(Name = "Сумма в исходной валюте")]
	[Column(TypeName = "decimal(10,2)")]
	public decimal OriginalAmount { get; set; }

	[StringLength(100)]
	[Display(Name = "Платежная система")]
	public string? PaymentGateway { get; set; }

	[DataType(DataType.DateTime)]
	[Display(Name = "Дата создания")]
	public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

	// Навигационные свойства
	public virtual Reservation? Booking { get; set; }
}

// Перечисление для способов оплаты
public enum PaymentMethod
{
	[Display(Name = "Кредитная карта")]
	CreditCard,

	[Display(Name = "Дебетовая карта")]
	DebitCard,

	[Display(Name = "Наличные")]
	Cash,

	[Display(Name = "Банковский перевод")]
	BankTransfer,

	[Display(Name = "Электронный кошелек")]
	EWallet,

	[Display(Name = "QR-код")]
	QRCode,

	[Display(Name = "Мобильный платеж")]
	MobilePayment
}

// Перечисление для статусов оплаты
public enum PaymentStatus
{
	[Display(Name = "Ожидает оплаты")]
	Pending,

	[Display(Name = "Оплачено")]
	Completed,

	[Display(Name = "Неуспешная оплата")]
	Failed,

	[Display(Name = "Возврат")]
	Refunded,

	[Display(Name = "Частично оплачено")]
	PartiallyPaid,

	[Display(Name = "Отменено")]
	Cancelled,

	[Display(Name = "Истек срок оплаты")]
	Expired
}

// Дополнительный класс для возвратов платежей
public class Refund
{
	public int Id { get; set; }

	[Required]
	[Display(Name = "ID платежа")]
	public int PaymentId { get; set; }

	[Required]
	[Column(TypeName = "decimal(10,2)")]
	[Range(0.01, 1000000, ErrorMessage = "Сумма возврата должна быть больше 0")]
	[Display(Name = "Сумма возврата (₽)")]
	public decimal RefundAmount { get; set; }

	[Required]
	[DataType(DataType.DateTime)]
	[Display(Name = "Дата возврата")]
	public DateTime RefundDate { get; set; } = DateTime.UtcNow;

	[Required]
	[StringLength(20)]
	[Display(Name = "Причина возврата")]
	public string Reason { get; set; } = string.Empty;

	[StringLength(50)]
	[Display(Name = "Номер возврата")]
	public string? RefundTransactionId { get; set; }

	[StringLength(500)]
	[Display(Name = "Комментарий")]
	public string? Notes { get; set; }

	[Display(Name = "Статус возврата")]
	public RefundStatus Status { get; set; } = RefundStatus.Pending;

	// Навигационные свойства
	public virtual Payment? Payment { get; set; }
}

public enum RefundStatus
{
	[Display(Name = "Ожидает обработки")]
	Pending,

	[Display(Name = "Успешно возвращено")]
	Completed,

	[Display(Name = "Ошибка возврата")]
	Failed,

	[Display(Name = "В процессе")]
	Processing
}