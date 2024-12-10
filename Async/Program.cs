using System.Diagnostics;

#region Menemen Yapma ve Mutfak İşleri Senaryosu

Stopwatch stopwatch = new Stopwatch();

// Senkron işlemler
Console.WriteLine("Senkron işlemler başlatılıyor...");
stopwatch.Start();
MenemenHazirlaSync();
MutfakTemizleSync();
stopwatch.Stop();
Console.WriteLine($"Senkron işlemler tamamlandı. Toplam Süre: {stopwatch.Elapsed.TotalSeconds} saniye");

stopwatch.Reset();
Console.WriteLine();

// Asenkron işlemler
Console.WriteLine("Asenkron işlemler başlatılıyor...");
stopwatch.Start();
Task menemenTask = MenemenHazirlaAsync();
Task mutfakTask = MutfakTemizleAsync();

await Task.WhenAll(menemenTask, mutfakTask);
stopwatch.Stop();
Console.WriteLine($"Asenkron işlemler tamamlandı. Toplam Süre: {stopwatch.Elapsed.TotalSeconds} saniye");

#endregion

#region Senkron Metotlar
static void MenemenHazirlaSync()
{
    Console.WriteLine("Domatesler doğranıyor...");
    Thread.Sleep(3000); 
    Console.WriteLine("Biberler doğranıyor...");
    Thread.Sleep(2000); 
    Console.WriteLine("Menemen pişiriliyor...");
    Thread.Sleep(5000); 
    Console.WriteLine("Menemen hazır!");
}

static void MutfakTemizleSync()
{
    Console.WriteLine("Tezgah siliniyor...");
    Thread.Sleep(2000); 
    Console.WriteLine("Bulaşıklar yıkanıyor...");
    Thread.Sleep(4000); 
    Console.WriteLine("Mutfak temizliği tamamlandı!");
}

#endregion

#region Asenkron Metotlar
static async Task MenemenHazirlaAsync()
{
    Console.WriteLine("Domatesler doğranıyor...");
    await Task.Delay(3000); 
    Console.WriteLine("Biberler doğranıyor...");
    await Task.Delay(2000); 
    Console.WriteLine("Menemen pişiriliyor...");
    await Task.Delay(5000);
    Console.WriteLine("Menemen hazır!");
}

static async Task MutfakTemizleAsync()
{
    Console.WriteLine("Tezgah siliniyor...");
    await Task.Delay(2000); 
    Console.WriteLine("Bulaşıklar yıkanıyor...");
    await Task.Delay(4000); 
    Console.WriteLine("Mutfak temizliği tamamlandı!");
}
#endregion

#region Task sınıfının tüm static methodları

static async Task SebzeDogra(string sebze)
{
    Console.WriteLine($"{sebze} doğranıyor...");
    await Task.Delay(3000);
    Console.WriteLine($"{sebze} doğrandı.");
}

static async Task TezgahTemizle()
{
    Console.WriteLine("Tezgah temizleniyor...");
    await Task.Delay(2000); 
    Console.WriteLine("Tezgah temizlendi.");
}

static async Task BulasikYika()
{
    Console.WriteLine("Bulaşıklar yıkanıyor...");
    await Task.Delay(4000); 
    Console.WriteLine("Bulaşıklar yıkandı.");
}

#endregion

#region Run

Console.WriteLine("Task.Run örneği başlatılıyor...");
await Task.Run(() => SebzeDogra("Domates"));
Console.WriteLine("Task.Run işlemi tamamlandı.");

#endregion

#region StartNew

Console.WriteLine("Task.Factory.StartNew örneği başlatılıyor...");
Task task = Task.Factory.StartNew(() => SebzeDogra("Biber").Wait());
task.Wait(); // Task'in tamamlanmasını bekliyoruz
Console.WriteLine("Task.Factory.StartNew işlemi tamamlandı.");

#endregion

#region WaitAll

// Verilen tüm task'ların tamamlanmasını bekler
Console.WriteLine("Task.WaitAll örneği başlatılıyor...");
Task domatesDograTask = Task.Run(() => SebzeDogra("Domates"));
Task tezgahTemizleTask = Task.Run(() => TezgahTemizle());
Task.WaitAll(domatesDograTask, tezgahTemizleTask); 
Console.WriteLine("Task.WaitAll işlemi tamamlandı.");

#endregion

#region WhenAll

Console.WriteLine("Task.WhenAll örneği başlatılıyor...");
Task biberDograTask = Task.Run(() => SebzeDogra("Biber"));
Task bulasikYikaTask = Task.Run(() => BulasikYika());
await Task.WhenAll(biberDograTask, bulasikYikaTask); 
Console.WriteLine("Task.WhenAll işlemi tamamlandı.");

#endregion

#region WaitAny

// Görevlerden herhangi biri tamamlanınca devam eder
Console.WriteLine("Task.WaitAny örneği başlatılıyor...");
Task task1 = Task.Run(() => SebzeDogra("Domates"));
Task task2 = Task.Run(() => TezgahTemizle());
Task.WaitAny(task1, task2); 
Console.WriteLine("Task.WaitAny işlemi tamamlandı. Bir iş tamamlandı, diğerleri devam ediyor.");

#endregion

#region WhenAny
// Görevlerden herhangi biri tamamlanınca devam eder

Console.WriteLine("Task.WhenAny örneği başlatılıyor...");
Task task3 = Task.Run(() => SebzeDogra("Biber"));
Task task4 = Task.Run(() => BulasikYika());
await Task.WhenAny(task3, task4); 
Console.WriteLine("Task.WhenAny işlemi tamamlandı. Bir iş tamamlandı, diğerleri devam ediyor.");

#endregion

#region Delay

Console.WriteLine("Task.Delay örneği başlatılıyor...");
await Task.Delay(2000); 
Console.WriteLine("2 saniye bekleme tamamlandı.");
Task task5 = Task.Run(() => SebzeDogra("Domates"));
Task task6 = Task.Run(() => BulasikYika());
await Task.WhenAll(task5, task6); 
Console.WriteLine("Task.Delay işlemi tamamlandı.");

#endregion

#region FromCanceled

static async Task SebzeDograIptalli(string sebze, CancellationToken cancellationToken)
{
    try
    {
        cancellationToken.ThrowIfCancellationRequested(); // İptal edildiğinde hata fırlatılır

        Console.WriteLine($"{sebze} doğranıyor...");
        await Task.Delay(3000, cancellationToken); // Doğrama işlemi 3 saniye sürüyor
        Console.WriteLine($"{sebze} doğrandı.");
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine($"{sebze} doğrama işlemi iptal edildi.");
        throw;
    }
}

CancellationTokenSource cts = new CancellationTokenSource();

try
{
    // İptali tetiklemek için 2 saniye sonra iptal ediyoruz
    cts.CancelAfter(2000);

    
    await SebzeDograIptalli("Domates", cts.Token);
}
catch (OperationCanceledException)
{
    var canceledTask = Task.FromCanceled(cts.Token);
    Console.WriteLine("Menemen hazırlama işlemi başarısız.");
}

#endregion

#region FromException

static async Task SebzeDogramaIslemi(string sebze)
{
    if (string.IsNullOrWhiteSpace(sebze))
    {
        throw new Exception("Geçersiz sebze adı");
    }

    Console.WriteLine($"{sebze} doğranıyor...");
    await Task.Delay(3000);
    Console.WriteLine($"{sebze} doğrandı.");
}

int sebzeId = -1;
Task dogramaTask;

if (sebzeId <= 0)
{
    // Geçersiz bir sebze ID'si için hata fırlatıyoruz
    dogramaTask = Task.FromException(new Exception("Geçersiz sebze ID'si"));
}
else
{
    // Geçerli bir sebze ID'si için işlem başlatıyoruz
    dogramaTask = SebzeDogramaIslemi("Domates");
}

try
{
    await dogramaTask; // Görev tamamlanmasını bekliyoruz
}
catch (Exception ex)
{
    Console.WriteLine($"Hata: {ex.Message}");
}

#endregion

#region FromResult

// Sebzenin geçerli olup olmadığını kontrol eden bir metot
static Task<bool> SebzeGecerliMi(string sebze)
{
    if (string.IsNullOrWhiteSpace(sebze))
    {
        Console.WriteLine("Geçersiz sebze adı.");
        return Task.FromResult(false);
    }

    Console.WriteLine($"{sebze} geçerli bir sebzedir.");
    return Task.FromResult(true);
}

static async Task MenemenIslemleri()
{
    // Geçersiz bir sebze ile kontrol
    bool sonuc = await SebzeGecerliMi("");
    if (!sonuc)
    {
        Console.WriteLine("Menemen hazırlama işlemi başarısız.");
        return;
    }

    // Geçerli sebze ile kontrol ve işlem
    Console.WriteLine("Menemen için işlemler başlatılıyor...");
    await SebzeDograma("Domates");
    Console.WriteLine("Menemen işlemi tamamlandı.");
}

// Sebze doğrama işlemi
static async Task SebzeDograma(string sebze)
{
    Console.WriteLine($"{sebze} doğranıyor...");
    await Task.Delay(3000); 
    Console.WriteLine($"{sebze} doğrandı.");
}

await MenemenIslemleri();

#endregion

#region Yield

static async Task SebzeDograUzunSureli(string sebze)
{
    Console.WriteLine($"{sebze} doğrama işlemi başlıyor...");

    for (int i = 1; i <= 5000; i++)
    {
        // 1000 adımdan sonra işlem diğer görevlerden ilerliyor
        if (i % 1000 == 0)
        {
            Console.WriteLine($"{sebze} doğrama ilerliyor: {i} adım tamamlandı...");
            await Task.Yield();
        }
    }

    Console.WriteLine($"{sebze} doğrama işlemi tamamlandı.");
}

await SebzeDograUzunSureli("Domates");

#endregion

#region GetAwaiter()

var sebzeDurumu = SebzeDogranmaDurumu("Domates");
Console.WriteLine(sebzeDurumu.GetAwaiter().GetResult()); // Sonuç senkron olarak beklenir ve işlem tamamlanınca döner

static async Task<string> SebzeDogranmaDurumu(string sebze)
{
    string durum = string.Empty;

    try
    {
        Console.WriteLine($"{sebze} doğranıyor...");
        await Task.Delay(3000);
        durum = $"{sebze} başarıyla doğrandı.";
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Bir hata oluştu: {ex.Message}");
        durum = $"{sebze} doğrama işlemi başarısız oldu.";
    }

    return durum;
}

#endregion

#region Asenkron İşlem ve Hata Yönetimi

try
{
    string sebzeAdi = "Domates";

    // Sebze doğrama işlemini başlat
    string dogramaSonucu = await SebzeDograAsync(sebzeAdi);

    Console.WriteLine($"İşlem Sonucu: {dogramaSonucu}");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Geçersiz İşlem Hatası: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Beklenmeyen Hata: {ex.Message}");
}

static async Task<string> SebzeDograAsync(string sebze)
{
    if (string.IsNullOrWhiteSpace(sebze))
    {
        throw new InvalidOperationException("Sebze adı boş olamaz.");
    }

    Console.WriteLine($"{sebze} doğranıyor...");
    await Task.Delay(3000); 

    if (sebze.ToLower() == "patlıcan")
    {
        throw new InvalidOperationException("Menemende patlıcan kullanılmaz!");
    }

    Console.WriteLine($"{sebze} başarıyla doğrandı.");
    return $"{sebze} doğrandı.";
}

#endregion

Console.ReadLine();