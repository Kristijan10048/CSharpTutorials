namespace ArrayRemoveInsert.tests
{
    using ArrayRemoveInsert;

    public class UnitTestsRemoveAt
    {
        [Fact]
        public void RemoveAt_middle_element_shifts_left_and_resizes()
        {
            char[] array = { 't', 'e', 's', 't' };

            Program.RemoveAt(1, ref array);

            Assert.Equal(new[] { 't', 's', 't' }, array);
        }

        [Fact]
        public void RemoveAt_first_element_shifts_everything_left()
        {
            char[] array = { 't', 'e', 's', 't' };

            Program.RemoveAt(0, ref array);

            Assert.Equal(new[] { 'e', 's', 't' }, array);
        }

        [Fact]
        public void RemoveAt_last_element_only_resizes_without_shifting()
        {
            char[] array = { 't', 'e', 's', 't' };

            Program.RemoveAt(3, ref array);

            Assert.Equal(new[] { 't', 'e', 's' }, array);
        }

        [Fact]
        public void RemoveAt_single_element_produces_empty_array()
        {
            char[] array = { 'a' };

            Program.RemoveAt(0, ref array);

            Assert.Empty(array);
            Assert.Equal(0, array.Length);
        }

        [Fact]
        public void RemoveAt_always_decreases_length_by_exactly_one()
        {
            char[] array = { 'a', 'b', 'c', 'd', 'e' };

            int originalLength = array.Length;

            Program.RemoveAt(2, ref array);

            Assert.Equal(originalLength - 1, array.Length);
        }

        [Fact]
        public void RemoveAt_preserves_order_and_values_of_remaining_elements()
        {
            char[] array = { 'a', 'b', 'c', 'd', 'e' };

            Program.RemoveAt(2, ref array); // remove 'c'

            Assert.Equal(new[] { 'a', 'b', 'd', 'e' }, array);
        }
    }
}
