using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Agriculture.DAL.Models;

namespace Agriculture.DAL.Configuration;

public class TreeTypeConfiguration :IEntityTypeConfiguration<TreeName>
{
    public void Configure(EntityTypeBuilder<TreeName> builder)
    { 
        builder
        .HasOne(tree => tree.Type) // navigation property
        .WithMany(type => type.TreeNames) // collection in TreeType
        .HasForeignKey(tree => tree.TypeId)
        .OnDelete(DeleteBehavior.Restrict);
        
    }
}