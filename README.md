# Sample.DeconstructorsForNonTuple

O tupple a o tom ako ich rozložiť v C# 7.0 sa popísalo veľa. [Napríklad ...](https://visualstudiomagazine.com/articles/2017/01/01/tuples-csharp-7.aspx)


Táto funkčnosť je super. Málokto však vie, že je možné rozkladať aj štandardné triedy, nie len tupple.

Nasledujúci príklad ukazuje ako triedu ```Person``` rozložiť do premenných ```firstName``` a ```lastName```.

```
public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }

    public void Deconstruct(out string firstName, out string lastName)
    {
        firstName = FirstName;
        lastName = LastName;
    }
}
```

Následne už môžte rozložiť vašu triedu do premenných.
```
var person = new Person()
{
    FirstName = "Janko",
    LastName = "Hraško"
};

var (firstName, lastName) = person;

firstName.Should().Be("Janko");
lastName.Should().Be("Hraško");
```
Vytvorili sme premennú ```person``` ako inštanciu triedy ```Person``` a rozložili ju do dvoch premenných.

V triede stačí definovať metódu ```Deconstruct(out T var1, ... , out T varN)```. Parametre musia byť definované ako out.
Môžme mať koľkokoľvek parametrov. Taktiež môžeme mať niekoľko preťažení danej metody.

Napríklad:
```
public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }

    public void Deconstruct(out string firstName, out string lastName)
    {
        firstName = FirstName;
        lastName = LastName;
    }

    public void Deconstruct(out string firstName, out string lastName, out int age)
    {
        firstName = FirstName;
        lastName = LastName;
        age = Age;
    }
}
```
Tá istá inštancia triedy ```Person``` môže byť rozložená oboma spôsobmi podľa kontextu.
```
var (firstName, lastName) = person;
(string firstName, string lastName, var age) = person;
```

# Nejednoznačné preťaženie
Jedná vec, ktorá by Vás mohla pomýliť je prípad, keď by ste chceli spraviť dve preťaženia s rovnakým počtom premenných,
ale inými typmi.
Nasledujúce preťaženie ```public void Deconstruct(out string firstName, out string lastName, out string email)```
by sme asi chceli považovať za možné.

Ale keď to vyskúšame, tak kompilátor zahlási nasledujúci chybu:
>The call is ambiguous between the following methods or properties: 'Person.Deconstruct(out string, out string, out int)' and 'Person.Deconstruct(out string, out string, out string)'

Dôvodom je, že je viacero spôsobov, akým sa dajú deklarovať premenné do ktorých sa to bude rozkladať. Preto nám neostáva nič iné, iba to akceptovať.

# Extensions
Ďalšiu výhodu, ktorú nám nová verzia jazyka prináša je, že môžme takto dekonštruvať aj triedy, ktoré nie sú naše. A to pomocou extension metódy.

Nasledujúci príklad ukáže, ako rozložiť inštanciu triedy ```Point``` do premenných ```x``` a ```y```.

Zadekladujme si extension metódu.
```
public static class PointExtensions
{
    public static void Deconstruct(this Point point, out int x, out int y)
    {
        x = point.X;
        y = point.Y;
    }
}
```
A následne už môžme rozkladať.
```
Point point = new Point(45, 85);

var (x, y) = point;

x.Should().Be(45);
y.Should().Be(85);
```

# Magic pattern-base C# features
Možno sa Vám to celé zdá magic. Menusím implementovať žiadne rozhranie, nemusím z ničoho dediť a ono to funguje. C# už vo viacerných novinkách využíva takzvaný pattern-base (alebo tiež convention-base) prístup. To znamená, že nadefinuje konvencie a tie keď dodržíme, tak to zrazu funguje :smile: