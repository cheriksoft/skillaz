REST-сервис по сокращению ссылок
================================
Список моих ссылок
------------------
```
GET /mylinks
```
Создать короткую ссылку (GET)
--------------------
```
GET /create?url=http://yandex.ru
```
Создать короткую ссылку (POST)
------------------------------
```
POST /create
```
### Тело запроса
```
{
  "Url": "http://yandex.ru"
}
```
Переход по ссылке с увеличением счетчика
----------------------------------------
```
GET /{urlId}
```
urlId - идентификатор ссылки, формат: `[0-9a-zA-Z]+`

