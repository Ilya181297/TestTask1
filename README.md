# Тестовое задание

Постановка:

Стоит задача разработать веб приложение по ведению справочника подразделений и персонала.
Приложение состоит из страницы, на которой слева отображается дерево подразделений. Подразделения имеют многоуровневую неограниченную вложенность.
Справа от дерева отображается таблица с работниками. Список работников меняется в зависимости от выбранного подразделения слева.
Приложение позволяет добавлять/изменять/удалять подразделения и работников. Также позволяет менять структуру подразделений и переносить работника между подразделениями.
Добавление и редактирование должно происходить в отдельном модальном окне или отдельной странице. Т. е. редактирование не в ячейке таблицы/дерева.
Подразделение имеет след. поля: Наименование, Дата формирования, Описание.
Работник имеет след. поля: ФИО, Дата рождения, Пол, Должность, Наличие водительских прав.


Требования:

Приложение должно быть написано на ASP.NET Core.
Все данные приложение должно хранить в базе данных MSSQL или PostgreSQL. Доступ к базе данных осуществлять через EntityFramework (можно LocalDB).
Визуальное оформление (использование стилей) не принципиально. Проверяться в первую очередь будет именно backend составляющая приложения.