namespace RSharp.Network.Codec
{
    public class ISAACCipher
    {
        private const int SIZE_LOG = 8;
        private const int SIZE = 1 << SIZE_LOG;
        private const int MASK = (SIZE - 1) << 2; /* for pseudorandom lookup */

        private readonly int[] _result;
        private readonly int[] _memory;

        private int _count;
        private int a; //The accumulator.
        private int b; //The last result
        private int c; //The counter, guarantees cycle is at least 2^^40

        public ISAACCipher(int[] seed)
        {
            _result = new int[SIZE];
            _memory = new int[SIZE];

            for (int i = 0; i < seed.Length; i++)
            {
                _result[i] = seed[i];
            }

            Init(true);
        }

        public int GetNextValue()
        {
            if (0 == _count--)
            {
                ISAAC();
                _count = SIZE - 1;
            }

            return _result[_count];
        }

        private void Init(bool flag)
        {
            int i;
            int a, b, c, d, e, f, g, h;
            a = b = c = d = e = f = g = h = unchecked((int)0x9e3779b9);                        /* the golden ratio */

            for (i = 0; i < 4; ++i)
            {
                a ^= b << 11; d += a; b += c;
                b ^= (int)((uint)c >> 2); e += b; c += d;
                c ^= d << 8; f += c; d += e;
                d ^= (int)((uint)e >> 16); g += d; e += f;
                e ^= f << 10; h += e; f += g;
                f ^= (int)((uint)g >> 4); a += f; g += h;
                g ^= h << 8; b += g; h += a;
                h ^= (int)((uint)a >> 9); c += h; a += b;
            }

            for (i = 0; i < SIZE; i += 8)
            {              /* fill in _memory[] with messy stuff */
                if (flag)
                {
                    a += _result[i]; b += _result[i + 1]; c += _result[i + 2]; d += _result[i + 3];
                    e += _result[i + 4]; f += _result[i + 5]; g += _result[i + 6]; h += _result[i + 7];
                }
                a ^= b << 11; d += a; b += c;
                b ^= (int)((uint)c >> 2); e += b; c += d;
                c ^= d << 8; f += c; d += e;
                d ^= (int)((uint)e >> 16); g += d; e += f;
                e ^= f << 10; h += e; f += g;
                f ^= (int)((uint)g >> 4); a += f; g += h;
                g ^= h << 8; b += g; h += a;
                h ^= (int)((uint)a >> 9); c += h; a += b;
                _memory[i] = a; _memory[i + 1] = b; _memory[i + 2] = c; _memory[i + 3] = d;
                _memory[i + 4] = e; _memory[i + 5] = f; _memory[i + 6] = g; _memory[i + 7] = h;
            }

            if (flag)
            {           /* second pass makes all of seed affect all of mem */
                for (i = 0; i < SIZE; i += 8)
                {
                    a += _memory[i]; b += _memory[i + 1]; c += _memory[i + 2]; d += _memory[i + 3];
                    e += _memory[i + 4]; f += _memory[i + 5]; g += _memory[i + 6]; h += _memory[i + 7];
                    a ^= b << 11; d += a; b += c;
                    b ^= (int)((uint)c >> 2); e += b; c += d;
                    c ^= d << 8; f += c; d += e;
                    d ^= (int)((uint)e >> 16); g += d; e += f;
                    e ^= f << 10; h += e; f += g;
                    f ^= (int)((uint)g >> 4); a += f; g += h;
                    g ^= h << 8; b += g; h += a;
                    h ^= (int)((uint)a >> 9); c += h; a += b;
                    _memory[i] = a; _memory[i + 1] = b; _memory[i + 2] = c; _memory[i + 3] = d;
                    _memory[i + 4] = e; _memory[i + 5] = f; _memory[i + 6] = g; _memory[i + 7] = h;
                }
            }

            ISAAC();
            _count = SIZE;
        }

        private void ISAAC()
        {
            int i, j, x, y;

            b += ++c;
            for (i = 0, j = SIZE / 2; i < SIZE / 2;)
            {
                x = _memory[i];
                a ^= a << 13;
                a += _memory[j++];
                _memory[i] = y = _memory[(x & MASK) >> 2] + a + b;
                _result[i++] = b = _memory[((y >> SIZE_LOG) & MASK) >> 2] + x;

                x = _memory[i];
                a ^= (int)((uint)a >> 6);
                a += _memory[j++];
                _memory[i] = y = _memory[(x & MASK) >> 2] + a + b;
                _result[i++] = b = _memory[((y >> SIZE_LOG) & MASK) >> 2] + x;

                x = _memory[i];
                a ^= a << 2;
                a += _memory[j++];
                _memory[i] = y = _memory[(x & MASK) >> 2] + a + b;
                _result[i++] = b = _memory[((y >> SIZE_LOG) & MASK) >> 2] + x;

                x = _memory[i];
                a ^= (int)((uint)a >> 16);
                a += _memory[j++];
                _memory[i] = y = _memory[(x & MASK) >> 2] + a + b;
                _result[i++] = b = _memory[((y >> SIZE_LOG) & MASK) >> 2] + x;
            }

            for (j = 0; j < SIZE / 2;)
            {
                x = _memory[i];
                a ^= a << 13;
                a += _memory[j++];
                _memory[i] = y = _memory[(x & MASK) >> 2] + a + b;
                _result[i++] = b = _memory[((y >> SIZE_LOG) & MASK) >> 2] + x;

                x = _memory[i];
                a ^= (int)((uint)a >> 6);
                a += _memory[j++];
                _memory[i] = y = _memory[(x & MASK) >> 2] + a + b;
                _result[i++] = b = _memory[((y >> SIZE_LOG) & MASK) >> 2] + x;

                x = _memory[i];
                a ^= a << 2;
                a += _memory[j++];
                _memory[i] = y = _memory[(x & MASK) >> 2] + a + b;
                _result[i++] = b = _memory[((y >> SIZE_LOG) & MASK) >> 2] + x;

                x = _memory[i];
                a ^= (int)((uint)a >> 16);
                a += _memory[j++];
                _memory[i] = y = _memory[(x & MASK) >> 2] + a + b;
                _result[i++] = b = _memory[((y >> SIZE_LOG) & MASK) >> 2] + x;
            }
        }
    }
}