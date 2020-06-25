//  Copyright (c) 2018 Demerzel Solutions Limited
//  This file is part of the Nethermind library.
// 
//  The Nethermind library is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  The Nethermind library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with the Nethermind. If not, see <http://www.gnu.org/licenses/>.
// 

using Nethermind.Abi;
using Nethermind.Blockchain.Contracts;
using Nethermind.Core;
using Nethermind.Core.Crypto;
using Nethermind.Core.Extensions;

namespace Nethermind.DepositContract.Contracts
{
    public sealed class DepositContract : Contract
    {
        public DepositContract(IAbiEncoder abiEncoder, Address contractAddress)
            : base(abiEncoder, contractAddress)
        {
        }

        public Transaction Deposit(
            Address sender,
            byte[] blsPublicKey,
            byte[] withdrawalCredentials,
            byte[] blsSignature,
            byte[] depositDataRoot)
            => GenerateTransaction<Transaction>(
                "deposit",
                sender,
                blsPublicKey,
                withdrawalCredentials,
                blsSignature,
                depositDataRoot);

        public Keccak DepositEventHash => GetEventHash("DepositEvent");

        public Transaction Deploy(Address senderAddress) =>
            new Transaction
            {
                Value = 0,
                Init = AbiDefinition.Bytecode,
                GasLimit = 2000000,
                GasPrice = 20.GWei(),
                SenderAddress = senderAddress
            };
    }
}