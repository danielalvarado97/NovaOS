# NovaOS

![NovaOS Logo](assets/logo.png)

---

# Descripció

NovaOS és un sistema operatiu experimental basat en CosmOS desenvolupat en C#.

El projecte té com a objectiu crear un shell funcional amb eines pròpies, gestió de fitxers, operacions matemàtiques, historial de comandes i una interfície moderna preparada per evolucionar cap a un sistema gràfic.


## Pantalla inicial

![Boot](assets/boot.png)

## Exemple de shell

```bash
NovaOS> guide

guide      - Mostra ajuda
origin     - Informació del sistema
peek       - Mostrar fitxers
forge      - Crear directori
```

## Exemple d'operacions matemàtiques

```bash
NovaOS> add 5 3
Resultat: 8

NovaOS> sqrt 25
Resultat: 5
```

## Exemple d'historial

```bash
NovaOS> history

1 - guide
2 - add 5 3
3 - sqrt 25
```

---

# Tecnologies utilitzades

## Llenguatges

- C#
- .NET 6

## Frameworks i eines

- CosmOS
- CosmosVFS
- VMware Workstation
- Visual Studio 2022
- Git & GitHub

## Altres recursos

- GitHub Desktop
- Cosmos Graphic Subsystem (CGS)


# Funcionalitats implementades

## Shell interactiu

NovaOS incorpora un shell funcional capaç d'executar diferents tipus de comandes.

---

## Sistema de fitxers

El sistema utilitza CosmosVFS per gestionar fitxers i directoris.

### Funcionalitats

- Navegació entre directoris
- Creació de carpetes
- Eliminació de carpetes
- Lectura de fitxers
- Escriptura de fitxers

### Exemple

```bash
forge projectes
jump projectes
write nota.txt Hola NovaOS
read nota.txt
```

---

## Operacions matemàtiques

NovaOS incorpora operacions aritmètiques bàsiques:

| Comanda | Funció |
|---|---|
| add | Suma |
| sub | Resta |
| mul | Multiplicació |
| div | Divisió |
| mod | Mòdul |
| sqrt | Arrel quadrada |

### Exemple

```bash
add 5 3
sqrt 49
```

---

## Sistema d'energia

| Comanda | Funció |
|---|---|
| shutdown | Apaga el sistema |
| reborn | Reinicia el sistema |

---

## Sons del sistema

NovaOS incorpora diferents sons:

| Acció | So |
|---|---|
| Arrencada del sistema | Melodia inicial |
| Comanda correcta | Beep curt |
| Error | Beep greu |

Els sons estan implementats amb:

```csharp
Console.Beep();
```

---

## Historial de comandes

El sistema desa les últimes cinc comandes executades.

| Comanda | Funció |
|---|---|
| history | Mostra historial |
| again [n] | Reexecuta una comanda |

### Exemple

```bash
history
again 2
```

---

## Configuració del teclat

NovaOS utilitza el layout espanyol del teclat:

```csharp
Sys.KeyboardManager.SetKeyLayout(
    new Sys.ScanMaps.ESStandardLayout()
);
```

---

# Comandes implementades

## Gestió de fitxers

| Comanda | Funció |
|---|---|
| peek | Mostra contingut d’un directori |
| jump | Canvia directori |
| forge | Crear directori |
| zap | Eliminar directori |
| read | Llegir fitxers |
| write | Escriure fitxers |

---

## Sistema

| Comanda | Funció |
|---|---|
| guide | Mostrar ajuda |
| origin | Informació del sistema |
| pulse | Memòria disponible |
| uptick | Temps d’execució |

---

## Utilitats

| Comanda | Funció |
|---|---|
| wipe | Netejar pantalla |
| say | Mostrar text |
| history | Historial de comandes |
| again | Reexecutar comanda |
| shutdown | Apagar sistema |
| reborn | Reiniciar sistema |

---

# Organització del codi

El codi està separat en funcions específiques per facilitar el manteniment:

- Funcions del shell
- Funcions matemàtiques
- Funcions de fitxers
- Funcions d’historial
- Funcions de so
- Funcions del sistema

---



## Membres del projecte

- Daniel Alvarado


## Contribucions

| Membre | Tasques |
|---|---|
| Daniel | Kernel, shell i sistema de fitxers |
|        | Comandes i calculadora |
|        | README i documentació |

---

# Contacte

- GitHub:
  https://github.com/danielalvarado97

---

# Llicència

Aquest projecte està sota la llicència MIT.

---

## Properes millores

- [ ] Implementar interfície gràfica completa
- [ ] Afegir suport per a finestres
- [ ] Millorar el sistema de fitxers
- [ ] Implementar usuaris i permisos
- [ ] Afegir gestor de processos
- [ ] Desenvolupar un editor de text
- [ ] Implementar multitasking
- [ ] Afegir suport de xarxa
- [ ] Millorar el sistema d'àudio
