using System;

namespace Hardware.DeviceAddress
{
    public class PsmAddress : Address, IAddress
    {
        public override string AddressString { get; protected set; }

        public override ushort AddressId { get; protected set; }

        public override string HardwareAcronym => "PSM";

        public override string HardwareDescription => "Proc Switch Matrix";

        public ushort MatrixColumn { get; private set; }
        public ushort MatrixRow { get; private set; }

        public override void UpdateAddressString(string newAddress)
        {
            AddressString = newAddress;
            var address = GetAddressPart(newAddress);
            if (!string.IsNullOrEmpty(address))
            {
                var matrixvalues = address.Split('/');
                ushort column, row;
                ushort.TryParse(matrixvalues[0], out column);
                ushort.TryParse(matrixvalues[1], out row);
                MatrixColumn = column;
                MatrixRow = row;

                AddressId = (ushort) parse_matrix_num(address);
            }
        }

        public void UpdateColumn(ushort column)
        {
            MatrixColumn = column;
            AddressString = HardwareAcronym + "-" + MatrixColumn + "/" + MatrixRow;
        }

        public void UpdateRow(ushort row)
        {
            MatrixRow = row;
            AddressString = HardwareAcronym + "-" + MatrixColumn + "/" + MatrixRow;
        }

        private static int parse_matrix_num(string num)
        {
            string[] cr_list = num.Split('/');
            return (32 + Int32.Parse(cr_list[0]) * 16 + Int32.Parse(cr_list[1]));
        }

        public AddressFactory.HardwareType HardwareType => AddressFactory.HardwareType.ProcSwitchMatrix;
    }
}