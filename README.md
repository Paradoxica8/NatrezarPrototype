# Natrezar Wastelands — Unity 2D Action RPG Prototype

A playable technical prototype of an isometric 2D action game, developed to test combat mechanics, control responsiveness, and stat balancing.

---

## 🎮 Core Loop
Town (Hub) ➔ Dungeon ➔ Combat Encounters ➔ Collecting Demonic Stones ➔ Returning to Town ➔ Progression (Weapon Upgrades).

---

## 🛠 Implemented Mechanics and Systems

### 1. Character and Combat System
* **Combo System:** Developed a modular character controller (State Machine) for smooth state transitions. Implemented a three-hit melee combo and dashes with invulnerability frames (i-frames).
* **Ranged Combat:** Added archery mechanics, including the ability to switch between daggers and a bow on the fly.
* **Status Effects (Debuffs):** Created a system for applying damage-over-time (bleeding) and crowd control (stunning) effects.

### 2. Artificial Intelligence (AI)
* Programmed two enemy archetypes: melee (closing the gap, attacking) and ranged (maintaining distance, kiting, shooting).
* Implemented pathfinding, player pursuit logic, and aggressive behavior phases.

### 3. Systems and Progression
* **Data Persistence:** Built a resource manager to track and save currency (Demonic Stones) across dungeon and town scenes.
* **User Interface (UI):** Configured dynamic displays for health, stamina, and resource collection counters. ---

## 💻 Tech Stack
* **Engine:** Unity
* **Programming Language:** C#
* **Physics:** Unity 2D Physics
* **Input:** Unity Input System (supports keyboard and gamepads)
* **Version Control:** Git / GitHub

---

## 📊 Game Design and Balancing


All combat system parameters—such as attack animation speeds, dash timings, enemy attack radii, hitboxes, and damage curves—are exposed in the Unity Inspector. This allowed for flexible adjustments to difficulty and "game feel" during playtesting, without the need to constantly modify the codebase.

# Пустоши Натрезара — Unity 2D Action RPG Prototype

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Играбельный технический прототип изометрического 2D-экшена, созданный для проверки боевых механик, отзывчивости управления и баланса параметров.

---

## 🎮 Основной игровой цикл (Core Loop)
Город (Хаб) ➔ Подземелье ➔ Боевые столкновения ➔ Сбор Демонических камней ➔ Возвращение в город ➔ Прогрессия (Улучшение оружия).

---

## 🛠 Реализованные механики и системы

### 1. Персонаж и Боевая система
* **Комбо-система:** Написал модульное управление персонажем (State Machine) для плавного переключения состояний. Реализовал трёхударное комбо ближнего боя и рывки с окнами неуязвимости.
* **Дистанционный бой:** Добавил стрельбу из лука и логику быстрого переключения между кинжалами и луком на ходу.
* **Эффекты (Дебаффы):** Написал систему наложения периодического урона (кровотечение) и контроля целей (оглушение).

### 2. Искусственный интеллект (AI)
* Запрограммировал два архетипа противников: ближний бой (микро-сближение, атака) и дальний бой (удержание дистанции, кайтинг, стрельба).
* Реализовал логику поиска пути, преследования игрока и фазы агрессии.

### 3. Системы и Прогрессия
* **Сохранение данных:** Создал менеджер ресурсов для переноса и сохранения валюты (Демонические камни) между сценами подземелья и города.
* **Интерфейс (UI):** Настроил отображение динамических показателей здоровья, запаса сил и счетчиков собранных ресурсов.

---

## 💻 Технологический стек
* **Движок:** Unity
* **Язык программирования:** C#
* **Физика:** Unity 2D Physics
* **Ввод:** Unity Input System (поддержка клавиатуры и геймпадов)
* **Контроль версий:** Git / GitHub

---

## 📊 Геймдизайн и настройка баланса


Все параметры боевой системы (скорость анимации ударов, тайминги рывков, радиус атаки врагов, хитбоксы и кривая урона) вынесены в инспектор Unity. Это позволило гибко настраивать сложность и "ощущение от игры" (game feel) в процессе плейтестов без постоянного изменения кодовой базы.

<img width="600" height="322" alt="918819983" src="https://github.com/user-attachments/assets/454da59f-5f8f-4fe0-8727-1f573a2c07a8" />
<img width="600" height="322" alt="918819971" src="https://github.com/user-attachments/assets/eac330fd-2fd4-4d43-b0b1-5cdc22495273" />
<img width="600" height="310" alt="918819961" src="https://github.com/user-attachments/assets/f63bb884-aa9f-4020-88b5-3cbd3dfbcf9d" />

