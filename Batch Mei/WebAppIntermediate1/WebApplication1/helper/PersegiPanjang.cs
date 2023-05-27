namespace WebApplication1.helper {
    public class PersegiPanjang {
        public int _panjang { get; set; }
        public int _lebar { get; set; }

        public int HitungLuas() {
            return _panjang * _lebar;
        }

        public int HitungLuasB() {
            var result =  _panjang * _lebar;
            return result;
        }

        public PersegiPanjang(int panjang, int lebar ) {
            _panjang = panjang;
            _lebar = lebar;
        }

        public PersegiPanjang()
        {
                
        }
    }
}
