namespace Юнит_тесты
{
    public class UnitTest1
    {
        [Fact]
        public void Contains()
        {
            //1-ое значение присутствует во 2 значении
            Assert.Contains("", " ");
        }

        [Fact]
        public void Equal()
        {
            //ожидаемое = результат
            Assert.Equal(15, 7+8);
        }

        [Fact]
        public void True()
        {
            //ожидаемое = результат
            Assert.True(false);
        }
    }
}