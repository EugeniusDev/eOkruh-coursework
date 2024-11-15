﻿namespace eOkruh.Common.DataProcessing
{
    internal static class NeoStrings
    {
        public static readonly string databaseLocalConnectionString = "bolt://localhost:7687";
        public static readonly string databaseUsername = "neo4j";
        public static readonly string databasePassword = "testPassword";
        public static readonly string userDatabase = "users";

        public static readonly string assignedByRelation = "ASSIGNED_BY";
        public static readonly string commandsRelation = "COMMANDS";
        public static readonly string registeredInRelation = "REGISTERED_IN";
        public static readonly string isPartOfRelation = "IS_PART_OF";
        public static readonly string hasRelation = "HAS";
    }
}
