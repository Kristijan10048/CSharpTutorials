namespace ArrayRemoveInsert.tests
{
    using ArrayRemoveInsert;

    public class UnitTestsInsertAndRound
    {
        // ---------------------------------------------------------------------
        // InsertAt(int index, ref char[] array, char element)
        // ---------------------------------------------------------------------

        [Fact]
        public void InsertAt_middle_element_shifts_right_and_places_element()
        {
            char[] array = { 'e', 's', 't' };

            Program.InsertAt(1, ref array, 'x');

            Assert.Equal(new[] { 'e', 'x', 's', 't' }, array);
        }

        [Fact]
        public void InsertAt_at_beginning_shifts_everything_right()
        {
            char[] array = { 'e', 's', 't' };

            Program.InsertAt(0, ref array, 't');

            Assert.Equal(new[] { 't', 'e', 's', 't' }, array);
        }

        [Fact]
        public void InsertAt_at_end_appends_without_shifting()
        {
            char[] array = { 't', 'e', 's' };

            Program.InsertAt(3, ref array, 't'); // index == current length -> append

            Assert.Equal(new[] { 't', 'e', 's', 't' }, array);
        }

        [Fact]
        public void InsertAt_into_empty_array_creates_single_element()
        {
            char[] array = Array.Empty<char>();

            Program.InsertAt(0, ref array, 'a');

            Assert.Equal(new[] { 'a' }, array);
        }

        [Fact]
        public void InsertAt_negative_index_throws_IndexOutOfRangeException()
        {
            char[] array = { 't', 'e', 's', 't' };

            Assert.Throws<IndexOutOfRangeException>(() => Program.InsertAt(-1, ref array, 'x'));
        }

        [Fact]
        public void InsertAt_beyond_end_throws_IndexOutOfRangeException()
        {
            char[] array = { 't', 'e', 's', 't' };

            Assert.Throws<IndexOutOfRangeException>(() => Program.InsertAt(5, ref array, 'x')); // > length
        }

        // ---------------------------------------------------------------------
        // Round(float n, int places)  -- round-half-up (adds 0.5 then truncates)
        // ---------------------------------------------------------------------

        [Fact]
        public void Round_two_decimal_places()
        {
            float result = Program.Round(975.68123f, 2);

            Assert.Equal(975.68f, result);
        }

        [Fact]
        public void Round_three_decimal_places()
        {
            float result = Program.Round(2.3456f, 3);

            Assert.Equal(2.346f, result);
        }

        [Fact]
        public void Round_zero_decimal_places_rounds_to_integer()
        {
            float result = Program.Round(3.9f, 0);

            Assert.Equal(4f, result);
        }

        [Fact]
        public void Round_no_fractional_part_stays_the_same()
        {
            float result = Program.Round(100f, 2);

            Assert.Equal(100f, result);
        }

        [Fact]
        public void Round_negative_value_rounds_toward_zero()
        {
            // (int) truncates toward zero, so negatives round the other way than positives.
            float result = Program.Round(-1.234f, 2);

            Assert.Equal(-1.22f, result);
        }

        // ---------------------------------------------------------------------
        // Integration: RemoveAt then InsertAt, as used in Main()
        // ---------------------------------------------------------------------

        [Fact]
        public void RemoveThenInsert_restores_same_length_and_expected_content()
        {
            char[] array = { 'a', 'b', 'c', 'd' };

            Program.RemoveAt(2, ref array);   // remove 'c' -> a b d (len 3)
            Assert.Equal(new[] { 'a', 'b', 'd' }, array);

            Program.InsertAt(1, ref array, 'z'); // insert 'z' at index 1 -> a z b d
            Assert.Equal(new[] { 'a', 'z', 'b', 'd' }, array);
        }
    }
}
