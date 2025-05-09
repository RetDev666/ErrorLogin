# ErrorLogger

## Огляд проекту

ErrorLogger — це легка система для централізованого збору та управління помилками, які виникають у програмних застосунках. Розроблена з використанням принципів чистої архітектури, система працює без бази даних, зберігаючи дані в пам'яті, що робить її ідеальною для відстеження помилок у середовищах розробки або невеликих проектах. ErrorLogger дозволяє розробникам швидко реагувати на помилки через інтеграцію з Telegram сповіщеннями.

## Функціональні можливості

- **Збір інформації про помилки**: Реєстрація деталей про помилки з різних джерел
- **Детальна інформація**: Збереження повідомлень, трасування стеку, джерела та статусу помилок
- **Управління статусом помилок**: Можливість змінювати і відстежувати стан помилок
- **Сповіщення через Telegram**: Миттєві повідомлення про нові помилки у вашу Telegram групу
- **RESTful API**: Зручний інтерфейс для інтеграції з різними системами
- **Swagger документація**: Інтерактивна документація API для розробників

## Архітектура проекту

Проект побудований на основі принципів чистої архітектури з чітким розподілом відповідальності:

### ErrorLogger.Domain
Містить бізнес-логіку, моделі та інтерфейси:
- Entities: Основні доменні об'єкти
- Interfaces: Контракти для сервісів і репозиторіїв
- Models: Моделі домену та DTO об'єкти

### ErrorLogger.Infrastructure
Містить реалізації інтерфейсів:
- Repositories: In-memory реалізації репозиторіїв
- Services: Сервіси для інтеграції з Telegram

### ErrorLogger.WebApi
Забезпечує HTTP інтерфейс:
- Controllers: Обробка API запитів
- Middleware: Глобальна обробка помилок
- Mappings: Перетворення між DTO і доменними моделями

## Технології

- **.NET 6**: Сучасна платформа для розробки застосунків
- **ASP.NET Core**: Для створення RESTful API
- **AutoMapper**: Для зручного мапінгу між об'єктами
- **Swagger/OpenAPI**: Для документації API
- **Telegram Bot API**: Для сповіщень у Telegram
- **In-Memory Storage**: Зберігання даних без зовнішньої бази даних

## Як почати роботу

### Передумови
- .NET 6 SDK або вище
- Токен Telegram бота для налаштування сповіщень

### Встановлення
1. Клонувати репозиторій:
   ```
   git clone https://github.com/RetDev666/ErrorLogin.git
   ```

2. Відкрити проект у Visual Studio або JetBrains Rider

3. Налаштувати Telegram сповіщення в `appsettings.json`:
   ```json
   "TelegramNotifications": {
     "BotToken": "Your_Bot_Token",
     "ChatId": "Your_Chat_Id"
   }
   ```

4. Запустити проект:
   ```
   dotnet run --project ErrorLogger.WebApi
   ```

### Використання API

Після запуску проекту API буде доступний за адресою `https://localhost:5001/swagger` (або іншим портом, залежно від налаштувань).

Основні ендпоінти:
- `POST /api/errors` - реєстрація нової помилки
- `GET /api/errors` - отримання списку всіх помилок
- `GET /api/errors/{id}` - отримання помилки за ID
- `PUT /api/errors/{id}/status` - оновлення статусу помилки
- `DELETE /api/errors/{id}` - видалення помилки

## Особливості реалізації без бази даних

ErrorLogger спеціально розроблений для роботи без бази даних, використовуючи натомість зберігання в пам'яті. Це має кілька переваг:

1. **Швидке розгортання** — не потрібно налаштовувати базу даних
2. **Простота використання** — мінімальні залежності та конфігурація
3. **Швидкодія** — операції з даними виконуються миттєво
4. **Портативність** — легко запускати в будь-якому середовищі

Зверніть увагу, що дані не зберігаються після перезапуску застосунку. Це зроблено навмисно для середовищ розробки та тестування. Якщо потрібне постійне зберігання, рекомендується розширити проект додаванням постійного сховища.

## Внесок у проект

1. Форкніть репозиторій
2. Створіть гілку для ваших змін:
   ```
   git checkout -b feature/amazing-feature
   ```
3. Закомітьте ваші зміни:
   ```
   git commit -m 'Add some amazing feature'
   ```
4. Зробіть пуш у вашу гілку:
   ```
   git push origin feature/amazing-feature
   ```
5. Створіть Pull Request

## Ліцензія

Цей проект розповсюджується під ліцензією MIT. Дивіться файл `LICENSE` для більш детальної інформації.

## Контакти

Якщо у вас виникли питання або пропозиції, будь ласка, відкрийте issue на GitHub або зв'яжіться з нами через GitHub профіль [RetDev666](https://github.com/RetDev666).

---

Дякуємо, що вибрали ErrorLogger для відстеження та управління помилками у ваших проектах!
