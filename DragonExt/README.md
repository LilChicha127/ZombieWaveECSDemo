# README

<details>
<summary><img width="16" src="https://gist.github.com/user-attachments/assets/94fcc1fb-22fe-4240-a78c-164722a2c7f4"> Read
</summary>

# Syntax extension for [DragonECS](https://github.com/DCFApixels/DragonECS)

> [!WARNING]
> This extension works slower than the standard syntax, so it is not recommended for writing performance-sensitive code. However, it is great for creating simple games and for prototyping.

This extension is designed to simplify syntax, reducing boilerplate and increasing readability.

The syntax for `Queries` and creating `EcsStaticMask` has been simplified, while the syntax for Entities has been extended to work in a more OOP style.

## Instalation
Copy the scripts to the project folder. The scripts have internal visibility, so they will not conflict with other assemblies.

## Examples

New syntax example:
```c#
public class NewVelocitySystem : IEcsRun
{
    Inc<Transform, Velocity>.Exc<FreezedTag> _mask;

    [EcsInject] EcsDefaultWorld _world;
    [EcsInject] TimeService _time;

    public void Run()
    {
        foreach (var e in (_world, _mask))
        {
            e.Get<Transform>().position += e.Get<Velocity>().value * _time.DeltaTime;
        }
    }
}
```
-------
Or:
```c#
public class NewVelocitySystem : IEcsRun
{
    [EcsInject] EcsDefaultWorld _world;
    [EcsInject] TimeService _time;

    public void Run()
    {
        foreach (var e in (_world, Inc<Transform, Velocity>.Exc<FreezedTag>.m))
        {
            e.Get<Transform>().position += e.Get<Velocity>().value * _time.DeltaTime;
        }
    }
}
```
-------

<details>
<summary>Old syntax:</summary>
  
```c#
public class VelocitySystem : IEcsRun
{
    class Aspect: EcsAspect
    {
        public EcsPool<Transform> transforms = Inc;
        public EcsPool<Velocity> velocities = Inc;
        public EcsTagPool<FreezedTag> freezedTags = Exc;
    }

    [EcsInject] EcsDefaultWorld _world;
    [EcsInject] TimeService _time;

    public void Run()
    {
        foreach (var e in _world.Where(out Aspect a))
        {
            a.transforms.Get(e).position += a.velocities.Get(e).value * _time.DeltaTime;
        }
    }
}
```
  
</details>
    
-------
[[OPEN]](https://dcfapixels.github.io/DragonECS-Mask_Generator_Online/) Mask generator for cases when there are not enough generic parameters for Inc or Exc. The generated code can simply replace the file EntLongQueryExtensions.cs.

</details>

<details>
<summary><img width="16" src="https://gist.github.com/user-attachments/assets/a9b31b21-01c3-4afa-83d2-302980b110d2"> Читать
</summary>

# Упрощенный синтаксис для [DragonECS](https://github.com/DCFApixels/DragonECS)

> [!WARNING]
> Новый синтаксис работает медленее, чем стандартный, поэтому не рекомендуется использовать в чувсвительном к скорости выполнения коде. Тем не менее, он отлично подходит для создания простых игр или прототипирования.

Это расширение разработано для упрощения синтаксиса, уменьшения бойлерплейта и повышения удобочитаемости.

Упрощен синтаксис для "Запросов" и создания "EcsStaticMask", а синтаксис для сущностей расширен для работы в более объектно-ориентированном стиле.

## Установка
Просто скопируйте скрипты в папку с проектом. Скрипты имеют модификатор видимости internal, поэтому не будет вызывать конфликт если используйется в других сборках.

## Примеры

Пример нового синтаксиса:
```c#
public class NewVelocitySystem : IEcsRun
{
    Inc<Transform, Velocity>.Exc<FreezedTag> _mask;

    [EcsInject] EcsDefaultWorld _world;
    [EcsInject] TimeService _time;

    public void Run()
    {
        foreach (var e in (_world, _mask))
        {
            e.Get<Transform>().position += e.Get<Velocity>().value * _time.DeltaTime;
        }
    }
}
```
-------
Or:
```c#
public class NewVelocitySystem : IEcsRun
{
    [EcsInject] EcsDefaultWorld _world;
    [EcsInject] TimeService _time;

    public void Run()
    {
        foreach (var e in (_world, Inc<Transform, Velocity>.Exc<FreezedTag>.m))
        {
            e.Get<Transform>().position += e.Get<Velocity>().value * _time.DeltaTime;
        }
    }
}
```
-------

<details>
<summary>Старый синтаксис:</summary>
  
```c#
public class VelocitySystem : IEcsRun
{
    class Aspect: EcsAspect
    {
        public EcsPool<Transform> transforms = Inc;
        public EcsPool<Velocity> velocities = Inc;
        public EcsTagPool<FreezedTag> freezedTags = Exc;
    }

    [EcsInject] EcsDefaultWorld _world;
    [EcsInject] TimeService _time;

    public void Run()
    {
        foreach (var e in _world.Where(out Aspect a))
        {
            a.transforms.Get(e).position += a.velocities.Get(e).value * _time.DeltaTime;
        }
    }
}
```
  
</details>
    
-------
[[ОТКРЫТЬ]](https://dcfapixels.github.io/DragonECS-Mask_Generator_Online/) Генератор маск на случай если не хватает встроенного колличества джинерик параметров для Inc и Exc. Сгенерированный код просто заменить с файлом EntLongQueryExtensions.cs.

</details>