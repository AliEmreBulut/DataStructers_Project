# 🐟 Marine Species Database & Algorithmic Data Structures

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

Bu proje, deniz canlılarına (özellikle Ege Denizi balıklarına) ait verileri yönetmek, aramak ve analiz etmek için temel bilgisayar bilimleri veri yapıları ve algoritmalarının **sıfırdan (from scratch)** implemente edildiği kapsamlı bir C# konsol uygulamasıdır. Proje, veri organizasyonu ve algoritma performans analizi (Time Complexity) konularında derinlemesine bir yaklaşım sunar.

## 🚀 Temel Özellikler (Core Features)

* **Özel İkili Arama Ağacı (Custom Binary Search Tree - BST):** Balık verileri ve bu balıklara ait detaylı metin bilgileri, kendi yazılmış `TreeNode` ve `EgeDeniziB_Ağacı` sınıfları kullanılarak bellekte hiyerarşik olarak organize edilir. Ağacın derinliği hesaplanarak dengeli (balanced) bir ağaç yapısı için teorik derinlik gereksinimleri analiz edilir.
* **Maksimum Yığın Veri Yapısı (Max Heap):** Öncelikli verileri hızlıca çekmek için bir `Max_heap` sınıfı tasarlanmıştır. Veriler yığına eklenirken (Trickle Up) ve çekilirken (Trickle Down) yığın kuralları algoritmik olarak korunur.
* **Hash Table ile Veri Güncelleme:** Belirli bir balık ismine `O(1)` karmaşıklığında anında erişmek ve açıklama bilgilerini güncellemek için Hash Table entegrasyonu sağlanmıştır.
* **Algoritma Performans Analizi (Benchmarking):** Dinamik olarak oluşturulan rastgele sayı dizileri üzerinde **Quick Sort** ve **Bubble Sort** algoritmaları koşturulur. 10 Milyon iterasyon üzerinden `.NET Stopwatch` kullanılarak milisaniye cinsinden donanım seviyesinde performans testi (benchmark) yapılır.

## 🧠 Mimari ve İşleyiş



[Image of a Binary Search Tree data structure]


Uygulama çalıştığında işlemler şu sırayla gerçekleşir:

1. **Veri Yükleme (File I/O):** `balik.txt` dosyasındaki veriler okunur. Özel ayırıcılar (seperators) kullanılarak metin parse edilir ve balıkların isimleri, boyutları ve yaşam alanları (Ege, Akdeniz vb.) sınıflandırılır.
2. **Ağaç Optimizasyonu:** Okunan veriler, arama maliyetini düşürmek adına önce bir Listeye alınır ve ardından ortadan bölünerek (recursive approach) ağaca eklenir. Bu sayede ağacın tek bir yöne uzaması engellenir.
3. **Filtreleme:** Kullanıcının girdiği iki harf (Örn: 'a' ve 'd') aralığındaki isimlere sahip kayıtlar ağaç üzerinde taranarak listelenir.
4. **Sıralama Algoritmaları İzlemesi:** Bubble ve Quick Sort algoritmaları, her bir yer değiştirme (swap) adımını konsola yazdırarak çalışma mantıklarını görselleştirir.

## ⚙️ Kurulum ve Çalıştırma

Projeyi lokal bilgisayarınızda derlemek ve incelemek için aşağıdaki adımları izleyebilirsiniz. *(Uygulamanın doğru çalışması için `balik.txt` dosyasının derleme dizininde bulunması gerekmektedir.)*

1. Repoyu klonlayın:
   ```bash
   git clone [https://github.com/AliEmreBulut/DataStructers_Project.git](https://github.com/AliEmreBulut/DataStructers_Project.git)
   ```
2. İlgili dizine gidin:
   ```bash
   cd DataStructers_Project/Proje_3
   ```
3. Projeyi çalıştırın:
   ```bash
   dotnet run
   ```
