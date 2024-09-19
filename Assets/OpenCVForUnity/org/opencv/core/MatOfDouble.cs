using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.UtilsModule;
using System;
using System.Collections.Generic;

namespace OpenCVForUnity.CoreModule
{
    public class MatOfDouble : Mat
    {
        // 64FC(x)
        private const int _depth = CvType.CV_64F;
        private const int _channels = 1;

        public MatOfDouble()
            : base()
        {

        }

        protected MatOfDouble(IntPtr addr)
            : base(addr)
        {

            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public static MatOfDouble fromNativeAddr(IntPtr addr)
        {
            return new MatOfDouble(addr);
        }

        public MatOfDouble(Mat m)
            : base(m, Range.all())
        {
            if (m != null)
                m.ThrowIfDisposed();


            if (!empty() && checkVector(_channels, _depth) < 0)
                throw new CvException("Incompatible Mat");
            //FIXME: do we need release() here?
        }

        public MatOfDouble(params double[] a)
            : base()
        {

            fromArray(a);
        }

        public void alloc(int elemNumber)
        {
            if (elemNumber > 0)
                base.create(elemNumber, 1, CvType.makeType(_depth, _channels));
        }

        public void fromArray(params double[] a)
        {
            if (a == null || a.Length == 0)
                return;
            int num = a.Length / _channels;
            alloc(num);

            if (isContinuous())
            {
                MatUtils.copyToMat<double>(a, this);
            }
            else
            {
                if (dims() <= 2)
                {
                    MatUtils.copyToMat<double>(a, this);
                }
                else
                {
                    put(0, 0, a); //TODO: check ret val!
                }
            }
        }

        public double[] toArray()
        {
            int num = checkVector(_channels, _depth);
            if (num < 0)
                throw new CvException("Native Mat has unexpected type or size: " + ToString());
            double[] a = new double[num * _channels];
            if (num == 0)
                return a;

            if (isContinuous())
            {
                MatUtils.copyFromMat<double>(this, a);
            }
            else
            {
                if (dims() <= 2)
                {
                    MatUtils.copyFromMat<double>(this, a);
                }
                else
                {
                    get(0, 0, a); //TODO: check ret val!
                }
            }
            return a;
        }

        public void fromList(List<double> lb)
        {
            if (lb == null || lb.Count == 0)
                return;

            int num = lb.Count / _channels;
            alloc(num);

            Converters.List_double_to_Mat(lb, this, num);
        }

        public List<double> toList()
        {
            int num = checkVector(_channels, _depth);
            if (num < 0)
                throw new CvException("Native Mat has unexpected type or size: " + ToString());

            List<double> a = new List<double>(num);
            for (int i = 0; i < num; i++)
            {
                a.Add(0);
            }
            Converters.Mat_to_List_double(this, a, num);
            return a;
        }
    }
}
