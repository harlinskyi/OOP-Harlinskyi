# Звіт з аналізу SOLID принципів (SRP, OCP) в Open-Source проєкті

## 1. Обраний проєкт
**Назва:** AutoMapper 
**Посилання на GitHub:** [https://github.com/AutoMapper/AutoMapper](https://github.com/AutoMapper/AutoMapper) 

---

## 2. Аналіз SRP (Single Responsibility Principle)

### 2.1. Приклади дотримання SRP 

#### Клас: `ResolutionContext` 
* **Відповідальність:** Зберігання контексту та стану під час виконання операції мапінгу.
* **Обґрунтування:** Клас діє як простий контейнер даних (Data Holder). Він не містить логіки перетворення або валідації, що мінімізує причини для його зміни.
* **Фрагмент коду:** 
```csharp
public class ResolutionContext : IInternalRuntimeLetter
{
    public object? SourceValue { get; }
    public object? DestinationValue { get; }
    public Type SourceType { get; }
    public Type DestinationType { get; }
}
```

Клас StringMapper:
* Відповідальність: виконання перетворення об'єктів у рядковий тип.
* Обґрунтування: клас ізольований і займається лише однією задачею — викликом методу ToString() для вхідного значення, не залежачи від загальної конфігурації системи.

### 2.2. Приклади порушення SRP
Клас MapperConfiguration: 
* Множинні відповідальності: Зберігання профілів конфігурації, валідація мап, кешування планів виконання та ініціалізація двигуна мапінгу.
* Проблеми: Даний клас є прикладом "God Object". Велика кількість відповідальностей робить його складним для тестування, а будь-яка зміна в логіці валідації вимагає модифікації всього об'ємного файлу.
* Фрагмент коду:
```csharp
public class MapperConfiguration : IConfigurationProvider
{
    private readonly List<ProfileMap> _profiles = new();
    public void AssertConfigurationIsValid() {}
    public LambdaExpression BuildExecutionPlan(TypePair typePair) {}
}
```

## 3. Аналіз OCP (Open/Closed Principle)

### 3.1. Приклади дотримання OCP
Сценарій/Модуль Система IObjectMapper: 
* Механізм розширення: Інтерфейси та патерн "Стратегія".
* Обґрунтування: Бібліотека дозволяє додавати підтримку нових типів даних без зміни існуючого коду. Користувач може реалізувати власний IObjectMapper і зареєструвати його в системі, не втручаючись у ядро.
* Фрагмент коду:
```csharp
public interface IObjectMapper
{
    bool IsMatch(TypePair context);
    Expression MapExpression(
        IConfigurationProvider configurationProvider,
        ProfileMap profileMap,
        IMemberMap memberMap,
        Expression sourceExpression, 
        Expression destExpression,
        Expression contextExpression
    );
}
```

### 3.2. Приклади порушення OCP
Сценарій/Модуль: Внутрішня логіка обробки Nullable типів 
* Проблема: Деякі базові типи обробляються через жорстко прописані (hardcoded) перевірки в методах побудови виразів.
* Наслідки: Додавання нової логіки для базових структур вимагає зміни існуючого коду ядра, оскільки ці частини закриті для зовнішнього розширення через використання модифікаторів internal.
* Фрагмент коду:
```csharp
if (sourceType.IsGenericType && sourceType.GetGenericTypeDefinition() == typeof(Nullable<>)) {}
```
## 4. Загальні висновки
Проєкт AutoMapper демонструє архітектуру, де принципи SRP та OCP є основою для розширюваності бібліотеки. Використання інтерфейсів для маперів дозволяє легко додавати нові типи трансформацій. Проте наявність великих конфігураційних класів вказує на певні труднощі в підтримці SRP в масштабах великих систем, що є компромісом між чистотою коду та продуктивністю.