using System.IO;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using UserControls.Common;
using UserControls.Models;

namespace UserControls.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly MainModel _model;

        private static readonly HttpClient _client = CreateClient(); // Crawling Tab

        public RelayCommand<string?> SearchCommand { get; } // Search Tab
        public RelayCommandAsync<string?> CrawlingCommand { get; } // Crawling Tab



        public MainViewModel()
        {
            _model = new MainModel();

            SearchCommand = new RelayCommand<string?>(Search, (searchText) => !string.IsNullOrWhiteSpace(searchText)); // Search Tab
            CrawlingCommand = new RelayCommandAsync<string?>(Crawl, (uri) => !string.IsNullOrWhiteSpace(uri)); // Crawling Tab
        }


        #region Methods


        private static HttpClient CreateClient() // Crawling Tab
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip
                                       | System.Net.DecompressionMethods.Deflate
            };

            var client = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(30) };
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/120.0 Safari/537.36");

            return client;
        }


        private async Task Crawl(string? uri) // Crawling Tab
        {
            if (!Uri.TryCreate(uri?.Trim(), UriKind.Absolute, out var crawlUri) || (crawlUri.Scheme != Uri.UriSchemeHttp && crawlUri.Scheme != Uri.UriSchemeHttps))
            {
                StatusText = "올바른 http/https URL을 입력하세요.";
                return;
            }

            IsBusy = true;
            StatusText = $"'{uri}' 크롤링 중...";
            string crawledData = "";

            try
            {
                using var response = await _client.GetAsync(uri);
                crawledData = await response.Content.ReadAsStringAsync();

                StatusText = $"{(int)response.StatusCode} {response.ReasonPhrase} / {crawledData.Length:N0}자";
            }
            catch (TaskCanceledException)
            {
                StatusText = "시간 초과";
            }
            catch (HttpRequestException ex)
            {
                StatusText = $"요청 실패: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }

            CrawlResult = crawledData;            
        }

        private void Search(string? searchText) // Search Tab
        {
            if (string.IsNullOrEmpty(searchText))
                return;

            StatusText = $"'{searchText}' 검색 중...";

            int count = 0;
            string [] dataAry = TextData.Split(new char[] { ' ' });
            foreach (var item in dataAry)
            {
                if (item.Contains(searchText))
                    count++;
            }

            Task.Delay(2000).ContinueWith(_ =>
            {
                StatusText = $"'{searchText}' 단어 {count}개 검색 완료.";
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        #endregion

        #region Properties

        public string StatusText // 공통
        {
            get => _model.StatusText;
            set => SetProperty(ref _model.StatusText, value);
        }


        public string SearchText // Search Tab
        {
            get => _model.SearchText;
            set
            {
                if (SetProperty(ref _model.SearchText, value))
                    SearchCommand.OnCanExecuteChanged();
            }
        }

        public string TextData // Search Tab
        {
            get
            {
                return $@"
님의 침묵

님은 갔습니다. 아아, 사랑하는 나의 님은 갔습니다.
푸른 산빛을 깨치고 단풍나무 숲을 향하여 난 작은 길을 걸어서 차마 떨어지고 가신 님이여.
황금의 꽃같이 굳고 빛나던 옛 맹세는 차디찬 塵埃(진애)가 되어서 한숨의 미풍에 날아갔습니다.
날카로운 첫 키스의 추억은 나의 운명의 지침을 돌려놓고 뒷걸음쳐서 사라졌습니다.
나는 향기로운 님의 말소리에 귀먹고, 꽃다운 님의 얼굴에 눈멀었습니다.
사랑도 사람의 일이라 만날 때에 미리 떠날 것을 염려하고 경계하지 아니한 것은 아니지만, 이별은 뜻밖의 일이 되고 놀란 가슴은 새로운 슬픔에 터집니다.
그러나 이별을 쓸데없는 눈물의 기원(水源)으로 만들고 마는 것은 스스로 사랑을 깨치는 것인 줄 알기 때문에, 걷잡을 수 없는 슬픔의 힘을 옮겨서 새 희망의 정수리배기에 들어부었습니다.
태양이 비치지 않는 밤에도 달이 떠오르는 것같이, 차마 님을 떠나지 못하는 나의 슬픔은 님의 얼굴을 붉게 물들입니다.
아아, 님이여! 나의 마음은 님의 사랑의 집이요, 나의 눈물은 님의 그늘의 이슬입니다.
나는 님의 사랑의 노래를 불러서 님의 귀를 밝히려 합니다.
님은 침묵을 지키고 계십니다.
아아, 님은 갔지마는 나는 님을 보내지 아니하였습니다.
제 곡조를 못 이기는 사랑의 노래는 님의 침묵을 맴돕니다.
";
            }
        }


        // Crawling Tab
        public string UriText
        {
            get => _model.UriText;
            set
            {
                if (SetProperty(ref _model.UriText, value))
                    CrawlingCommand.OnCanExecuteChanged();
            }
        }

        public string CrawlResult
        {
            get => _model.CrawlResult;
            set => SetProperty(ref _model.CrawlResult, value);
        }

        public bool IsBusy // Crawling Tab
        {
            get => _model.IsBusy;
            set => SetProperty(ref _model.IsBusy, value);
        }


        public double Quantity
        {
            get => _model.Quantity;
            set => SetProperty(ref _model.Quantity, value);
        }

        public string Password
        {
            get => _model.Password;
            set => SetProperty(ref _model.Password, value);
        }

        public int CurrentPage
        {
            get => _model.CurrentPage;
            set => SetProperty(ref _model.CurrentPage, value);
        }

        public int TotalPages
        {
            get => _model.TotalPages;
            set => SetProperty(ref _model.TotalPages, value);
        }


        public bool NotificationsEnabled
        {
            get => _model.NotificationsEnabled;
            set => SetProperty(ref _model.NotificationsEnabled, value);
        }

        public int Score
        {
            get => _model.Score;
            set => SetProperty(ref _model.Score, value);
        }

        public string FilePath
        {
            get => _model.FilePath;
            set => SetProperty(ref _model.FilePath, value);
        }

        public string FolderPath
        {
            get => _model.FolderPath;
            set => SetProperty(ref _model.FolderPath, value);
        }

        #endregion
    }
}
