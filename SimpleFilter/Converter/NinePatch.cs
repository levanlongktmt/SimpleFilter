using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SimpleFilter.Converter
{
    public class NinePatch
    {
        private int width;
        private int height;

        private WriteableBitmap imageRoot;
        private WriteableBitmap mWithOutLine;
        private WriteableBitmap mFinalBitMap;

        private int bottomLineStart = -1;
        private int bottomLineEnd = -1;
        private int rightLineStart = -1;
        private int rightLineEnd = -1;

        private List<LineNinePatch> topLine = new List<LineNinePatch>();
        private List<LineNinePatch> leftLine = new List<LineNinePatch>();
        //We need to remove the border, soo 2 pixel less
        private int whitePixelTopLine = -2;
        private int whitePixelLeftLine = -2;
        private int numberOfBlackBlockTopLine = 0;
        private int numberOfBlackBlockLeftLine = 0;

        public NinePatch(BitmapImage image, int width, int height)
        {
            this.imageRoot = new WriteableBitmap(image);
            int mWidth = image.PixelWidth;
            int mHeight = image.PixelHeight;
            this.width = width > mWidth ? width : mWidth;
            this.height = height > mHeight ? height : mHeight;
        }

        public Thickness getPadding()
        {

            getRightLine();
            getBottomLine();

            return new Thickness(bottomLineStart, rightLineStart, this.imageRoot.PixelWidth - (bottomLineEnd - bottomLineStart), this.imageRoot.PixelHeight - (rightLineEnd - rightLineStart));

        }

        public WriteableBitmap getBitmap9Patch()
        {

            getTopLine();
            getLeftLine();

            mWithOutLine = WriteableBitmapExtensions.Crop(imageRoot, new Rect(1, 1, imageRoot.PixelWidth - 2, imageRoot.PixelHeight - 2));
            mFinalBitMap = new WriteableBitmap(this.width, this.height);

            //LINE

            LineNinePatch mLine = null;
            LineNinePatch mColumn = null;
            int xCurrentBigImage = 0;
            int yCurrentBigImage = 0;
            for (int i = 0; i < leftLine.Count(); i++)
            {

                mLine = leftLine[i];

                //COLUMN
                for (int x = 0; x < topLine.Count(); x++)
                {
                    mColumn = topLine[x];

                    if (!mLine.isBlack && !mColumn.isBlack)
                    {
                        mFinalBitMap.Blit(new Rect(xCurrentBigImage, yCurrentBigImage, mColumn.endLine - mColumn.startLine, mLine.endLine - mLine.startLine), mWithOutLine, new Rect(mColumn.startLine, mLine.startLine, mColumn.endLine - mColumn.startLine, mLine.endLine - mLine.startLine));
                        xCurrentBigImage += mColumn.endLine - mColumn.startLine;
                    }
                    else if (!mLine.isBlack && mColumn.isBlack)
                    {
                        mFinalBitMap.Blit(new Rect(xCurrentBigImage, yCurrentBigImage, (width - whitePixelTopLine) / numberOfBlackBlockTopLine, mLine.endLine - mLine.startLine), mWithOutLine, new Rect(mColumn.startLine, mLine.startLine, mColumn.endLine - mColumn.startLine, mLine.endLine - mLine.startLine));
                        xCurrentBigImage += (width - whitePixelTopLine) / numberOfBlackBlockTopLine;
                    }
                    else if (mLine.isBlack && !mColumn.isBlack)
                    {
                        mFinalBitMap.Blit(new Rect(xCurrentBigImage, yCurrentBigImage, mColumn.endLine - mColumn.startLine, (height - whitePixelLeftLine) / numberOfBlackBlockLeftLine), mWithOutLine, new Rect(mColumn.startLine, mLine.startLine, mColumn.endLine - mColumn.startLine, mLine.endLine - mLine.startLine));
                        xCurrentBigImage += mColumn.endLine - mColumn.startLine;
                    }
                    else
                    {
                        mFinalBitMap.Blit(new Rect(xCurrentBigImage, yCurrentBigImage, (width - whitePixelTopLine) / numberOfBlackBlockTopLine, (height - whitePixelLeftLine) / numberOfBlackBlockLeftLine), mWithOutLine, new Rect(mColumn.startLine, mLine.startLine, mColumn.endLine - mColumn.startLine, mLine.endLine - mLine.startLine));
                        xCurrentBigImage += (width - whitePixelTopLine) / numberOfBlackBlockTopLine;
                    }
                }

                xCurrentBigImage = 0;

                if (mLine.isBlack)
                {
                    yCurrentBigImage += (height - whitePixelLeftLine) / numberOfBlackBlockLeftLine;
                }
                else
                {
                    yCurrentBigImage += mLine.endLine - mLine.startLine;
                }

            }

            return mFinalBitMap;
        }


        private void getLeftLine()
        {
            LineNinePatch mLine = new LineNinePatch();
            mLine.isHorizontal = false;

            bool isPixelBlack = false;
            for (int i = 0; i < imageRoot.PixelHeight; i++)
            {
                isPixelBlack = imageRoot.GetPixel(0, i).Equals(Colors.Black);
                if (!isPixelBlack)
                {
                    whitePixelLeftLine++;
                }

                if (mLine.startLine == -1)
                {
                    mLine.startLine = i;

                    if (isPixelBlack)
                    {
                        mLine.isBlack = true;
                    }
                    else
                    {
                        mLine.isBlack = false;
                    }
                }
                else if ((!mLine.isBlack && isPixelBlack) || (mLine.isBlack && !isPixelBlack))
                {
                    mLine.endLine = i - 1;
                    isPixelBlack = mLine.isBlack;
                    if (isPixelBlack)
                    {
                        numberOfBlackBlockLeftLine++;
                    }
                    leftLine.Add(mLine);

                    mLine = new LineNinePatch();
                    mLine.isHorizontal = false;
                    mLine.startLine = i - 1;
                    mLine.isBlack = !isPixelBlack;
                }
            }

            if (mLine.endLine == -1)
            {
                if (mLine.isBlack)
                {
                    numberOfBlackBlockLeftLine++;
                }

                mLine.endLine = imageRoot.PixelWidth - 2;
                leftLine.Add(mLine);
            }
        }

        private void getRightLine()
        {
            for (int i = 0; i < imageRoot.PixelHeight; i++)
            {
                if (rightLineStart == -1 && imageRoot.GetPixel(imageRoot.PixelWidth - 1, i).Equals(Colors.Black))
                {
                    rightLineStart = i - 1;
                }
                else if (rightLineStart != -1 && rightLineEnd == -1 && !imageRoot.GetPixel(imageRoot.PixelWidth - 1, i).Equals(Colors.Black))
                {
                    rightLineEnd = i - 1;
                    break;
                }
            }
        }

        private void getTopLine()
        {
            LineNinePatch mLine = new LineNinePatch();
            mLine.isHorizontal = true;
            bool isPixelBlack = false;

            for (int i = 0; i < imageRoot.PixelWidth; i++)
            {

                isPixelBlack = imageRoot.GetPixel(i, 0).Equals(Colors.Black);

                if (!isPixelBlack)
                {
                    whitePixelTopLine++;
                }


                if (mLine.startLine == -1)
                {
                    mLine.startLine = i;

                    if (isPixelBlack)
                    {
                        mLine.isBlack = true;
                    }
                    else
                    {
                        mLine.isBlack = false;
                    }
                }
                else if ((!mLine.isBlack && isPixelBlack) || (mLine.isBlack && !isPixelBlack))
                {
                    mLine.endLine = i - 1;
                    isPixelBlack = mLine.isBlack;
                    if (isPixelBlack)
                    {
                        numberOfBlackBlockTopLine++;
                    }
                    topLine.Add(mLine);

                    mLine = new LineNinePatch();
                    mLine.isHorizontal = true;
                    mLine.startLine = i - 1;
                    mLine.isBlack = !isPixelBlack;
                }
            }

            if (mLine.endLine == -1)
            {
                if (mLine.isBlack)
                {
                    numberOfBlackBlockTopLine++;
                }
                mLine.endLine = imageRoot.PixelWidth - 2;
                topLine.Add(mLine);
            }
        }

        private void getBottomLine()
        {
            for (int i = 0; i < imageRoot.PixelWidth; i++)
            {
                if (bottomLineStart == -1 && imageRoot.GetPixel(i, imageRoot.PixelHeight - 1).Equals(Colors.Black))
                {
                    bottomLineStart = i - 1;
                }
                else if (bottomLineStart != -1 && bottomLineEnd == -1 && !imageRoot.GetPixel(i, imageRoot.PixelHeight - 1).Equals(Colors.Black))
                {
                    bottomLineEnd = i - 1;
                    break;
                }
            }
        }

        protected class LineNinePatch
        {
            public int startLine = -1;
            public int endLine = -1;
            public bool isHorizontal = false;
            public bool isBlack = false;

        }
    }
}
