# dotNet_playground
# จุดประสงค์
ตั้งใจให้ผู้อ่านได้แนวทางการพัฒนาและดูแล Web API ของเราได้อย่างเหมาะสม ยืดหยุ่น รองรับการเปลี่ยนแปลงที่เกิดขึ้นจากความต้องการของลูกค้า และติดตั้งบน Servers ต่างๆได้อย่างสะดวกนะครับ

เนื้อหาเลยจะไม่ได้เน้นลงลึกในแต่ละหัวข้อ แต่จะรวบรวมหัวข้อที่คิดว่าจำเป็นในการพัฒนา Web API ที่จำเป็นให้ได้มากที่สุดแทนครับผม มองเป็น Keyword ไว้ตามไปศึกษาเพิ่มเติมกันต่อก็ได้นะครับ เย้ ^^
- Article: https://medium.com/t-t-software-solution/clean-architecture-from-scratch-with-net7-187f18b6accd

- ติดตั้งโปรแกรมที่จำเป็นกับการพัฒนา Web API ด้วย .NET 

- แนะนำ VS Code Extension เพื่อทำให้การพัฒนา .NET 6 ได้สะดวกขึ้น
- เข้าใจการใช้ .NET CLI ในการจัดการโปรเจ็ค, ฐานข้อมูล และการรันโปรแกรม
- ศึกษาโครงสร้างของไฟล์ใน .NET 6 โปรเจ็ค
- เข้าใจการ Debugging ด้วย VS Code
- เข้าใจแนวทางการพัฒนาโปรเจ็คให้ Source Code มีความยืดหยุ่นและดูแลรักษาได้ง่าย ด้วย - Interface, Dependency Injection, Service Lifetimes, Clean Architecture
- หัดติดตั้งและเชื่อมต่อฐานข้อมูล PostgreSQL ด้วย Docker และ DBeaver
- หัดติดตั้งและเชื่อมต่อฐานข้อมูล MongoDB ด้วย Docker และ MongoDB Compass
- ทดลองใช้ Swagger OpenAPI
- ทดลองใช้งาน Postman ในการเรียก Web API
- ศึกษาเรื่อง IOption Pattern ในการเรียกใช้ Configuration File, Environment Variable
- ทดลองใช้งาน Entity Framework ด้วย Code First Migration, Seeding Data และ Database Context ด้วยการเชื่อมต่อไปที่ PostgreSQL
- ศึกษาเรื่อง Logging ด้วย Serilog และ Sink ด้วยการเชื่อมต่อไปที่ MongoDB
- ศึกษาเรื่อง User Authentication ด้วย .NET Core Identity and JSON Web Tokens
- ศึกษาการทำ Unit Testing ด้วย xUnit
- ทดลองพัฒนา Backgroud/Schedule Process ด้วย Hangfire
- ทดลองสร้างระบบ Health Check UI ด้วย AspNetCore.Diagnostics.HealthChecks
- ปรียบเทียบสิ่งที่ทำไปทั้งหมดในบทความกับ The Twelve Factors App

---
# บทที่ 4 - source code structure
## 1. domain - ดูแล entity database 
## 2. core - main logic หลักของระบบใช้ในการกำหนด interface ใน part ของ Infra
## 3. Infra - นำ Interface จาก Core มา Implement , ทำให้ระบบเชื่อมต่อกับ thrid-party 
## 4. Api - เป็นเส้นเชื่อมต่อกับข้อมูลภายในระบบ

## cmd for 
### dotnet new classlib - เป็นการสร้าง lib ใหม่ใน folder; -n เป็นการ ตั้งชื่อ folder
### dotnet sln add - เป็นการแอด solution 

### dotnet add reference - เป็นการ reference this folder(project) to 'API'   

```
    -> หมายถึงการดึงตามลำดับ Architecture
    core -> domain
    Infra -> domain , core
    Api -> Infra , Core
```
### dotnet watch run - ทำการ hot reload // ต้องทำการ run ใน project API
---

## บทที่ 5 - debug 
กดปุ่มบนมุมขวา แล้วจะมีไฟล์ .vscode launch , tasks เกิดขึ้น
จากนั้นเราสามารถกดเลือกได้เลย ว่าอยากตรวจเช็คตรงไหน

---
## บทที่ 6 - Configuration (การกำหนดค่า)
เกิดจากการที่เรากำหนดจาก Configuration data ex. Settings files เช่น appsettings.json , Environment Variables , ...

### Iconfiguration - เป็นศูนย์กลางในการดึงข้อมูลจากหลายๆ source มาแสลงผล
```
    1.appsetting.json
    2.appsettings.{env}.json เช่น appsetting.Production.json นั้นเป็นการอิง File จาก Env Variable `ASPNETCORE_ENV`
    3.Env
    4.Cmd-line arguments
```
ตัวอย่าง appsetting.json

```
    "Position" :{
        "Title" : "Editor",
        "Name" : "Joe Smith",
    }
```

เราสามารถแปลง Env_Variables ด้วยการ __ เป็นตัวคั่น ของ Property
BASH
 ```
    export Position__Title="Linux"
    export Position__Name="Bash"
 ```

เมื่อเราทำการ dotnet run แล้วจะเป็นการ
- จาก appsettings.json -> Position:Title="Editor" -> เปลี่ยนเป็น Env_Variable -> Position__Title="Linux" ใน Bash

## Configuration - Options pattern
| เป็นการทำให้สามารถแก้ไข Configuration Item ได้
เราสามารถอ้างอิงถึง Configuration Items ได้จาก Strong Type ครับ ซึ่ง Option Pattern ช่วยในเรื่องนี้ได้ , ยังช่วยในการจัดการ Value ของ Configuration ของเราเป็น Type ไหน
### json file
```
    "Position" :{
        "Title" : "Editor",
        "Name" : "Joe Smith",
    }
```
### C#
```
public class PositionOptions
{
    public const string Position = "Position";
    public string Title {get; set;} // Title = "Editor"
    public string Name {get; set;} // Name = "Joe Smith"
}
```
คุณสมบัติของ class ที่จะไม่เป็น Option Class 
    - Abstract Class
    - Public Read Write Properties สำหรับ Type ถูก Bind with Configuration Item

![ภาพปลากรอบ_1](https://miro.medium.com/v2/resize:fit:4800/format:webp/1*IzwsWjSXKMY4AVahRKjE7g.png)

---
