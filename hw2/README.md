**God Object (Об'єкт-Бог)** — це об'єкт, який зосереджує в собі занадто багато функцій, стаючи центральним вузлом усієї логіки програми. 

### Основні характеристики:
* **Порушення інкапсуляції:** Клас містить логіку, що належить до абсолютно різних доменних областей.
* **Величезний обсяг коду:** Тисячі рядків коду, які важко навігувати та підтримувати.
* **Висока зв'язність (High Coupling):** Зміни в одній частині системи неминуче зачіпають цей клас, що створює ризик регресійних помилок.
* **Складність тестування:** Через велику кількість залежностей та внутрішніх станів ізольоване тестування стає майже неможливим.

## 2. Порушення принципу SRP

**Single Responsibility Principle (SRP)** стверджує, що клас повинен мати лише одну причину для зміни.

### Приклад коду з порушенням (C#)
У наведеному прикладі клас `UserActivityManager` виконує три різні ролі: керує даними, генерує звіти та працює з поштою.

```csharp
public class UserActivityManager
{
    public void RegisterUser(string email)
    {
        Console.WriteLine($"User {email} saved to database.");
    }

    public void GenerateActivityReport()
    {
        Console.WriteLine("Generating PDF activity report...");
    }

    public void SendNotification(string message)
    {
        Console.WriteLine("Email sent.");
    }
}
```

## 3. Рефакторинг (Дотримання SRP)
Для виправлення анти-патерна ми розділяємо обов'язки між окремими спеціалізованими класами.

Результат рефакторингу
* UserRepository — відповідає виключно за збереження даних.
* ReportGenerator — займається лише логікою формування звітів.
* NotificationService — відповідає за відправку повідомлень.

```csharp
public class UserRepository
{
    public void Save(string email) {}
}

public class ReportGenerator
{
    public void CreateReport() {}
}

public class NotificationService
{
    public void Send(string message) {}
}
```

**Переваги такого підходу**:
* Тестованість: Кожен клас можна протестувати окремо.
* Гнучкість: Ми можемо змінити формат звіту, не зачіпаючи логіку реєстрації користувачів.
* Чистота коду: Код стає зрозумілим та легким для читання іншими розробниками.